using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongApi.Models;
using SongAPI.Data;
using SongAPI.Models;

namespace SongAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly SongDbContext _context;

        public SongController(SongDbContext context)
        {
            _context = context;
        }

        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs
                            .Include(s => s.Categories)
                            .ToListAsync();
        }

        // GET: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Song/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, UpdateSongDto songDto)
        {
            if (id != songDto.Id)
            {
                return BadRequest();
            }

            var song = await _context.Songs.Include(s => s.Categories).FirstOrDefaultAsync(s => s.Id == id);

            if(song == null) {
                return NotFound();
            }

            // Update artist, title and length
            song.Artist = songDto.Artist;
            song.Title = songDto.Title;
            song.Length = songDto.Length;

            // Get the new categories based on id's
            var categories = await _context.Categories
                                .Where(c => songDto.CategoryIds.Contains(c.Id))
                                .ToListAsync();

            // Udate the categories
            song.Categories = categories;
            
            _context.Entry(song).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        
            return NoContent();
        }

        // POST: api/Song
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(CreateSongDto songDto)
        {
            // Create the Song entity based on the DTO
            var song = new Song
            {
                Artist = songDto.Artist,
                Title = songDto.Title,
                Length = songDto.Length
            };

            // Retrieve the categories based on the IDs
            var categories = await _context.Categories
                            .Where(c => songDto.CategoryIds.Contains(c.Id))
                            .ToListAsync();

            song.Categories = categories;

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.Id }, song);
        }

        // DELETE: api/Song/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
