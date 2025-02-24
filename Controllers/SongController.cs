using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var song = await _context.Songs
                            .Include(s => s.Categories)
                            .FirstOrDefaultAsync(s =>s.Id == id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Song/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, CreateSongDto songDto)
        {
            if (id != songDto.Id)
            {
                return BadRequest();
            }

            var song = await _context.Songs
                            .Include(s => s.Categories)
                            .FirstOrDefaultAsync(s => s.Id == id);

            if(song == null) {
                return NotFound();
            }

            // Update song-data
            song.Artist = songDto.Artist;
            song.Title = songDto.Title;
            song.Length = songDto.Length;

            // Update categories based on Id's in req
            song.Categories = await _context.Categories
                                        .Where(c => songDto.Categories.Contains(c.Id))
                                        .ToListAsync();


            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Song
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateSongDto>> PostSong(CreateSongDto songDto)
        {
            var song = new Song 
            {
                Artist = songDto.Artist,
                Title = songDto.Title,
                Length = songDto.Length
            };

            // Get the categories based on Id's in req
            song.Categories = await _context.Categories
                                        .Where(c => songDto.Categories.Contains(c.Id))
                                        .ToListAsync();

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
