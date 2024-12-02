using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.Models;

namespace WAD.CODEBASE._00016643.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewspaperController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewspaperController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Newspapaer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Newspaper>>> GetAll()
        {
            return await _context.Newspapers.Include(n => n.Articles).ToListAsync();
        }

        // GET: api/Newspaper/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Newspaper), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var foundNewspaper = await _context.Newspapers.Include(n => n.Articles).FirstOrDefaultAsync(n => n.NewspaperId == id);
            return foundNewspaper != null ? Ok(foundNewspaper) : NotFound();
        }

        [HttpGet("ignore/{id}")]
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task<IActionResult> GetNewspaper(int id)
        {
            var foundNewspaper = await _context.Newspapers.FindAsync(id);
            return foundNewspaper != null ? Ok(foundNewspaper) : NotFound();
        }

        // POST: api/Newspaper
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Newspaper newspaper)
        {
            await _context.Newspapers.AddAsync(newspaper);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNewspaper), new { id = newspaper.NewspaperId }, newspaper);
        }

        // PUT: api/Newspaper/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Newspaper newspaper)
        {
            if (id != newspaper.NewspaperId) return BadRequest();
            _context.Entry(newspaper).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Newspaper/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var newspaperToDelete = await _context.Newspapers.FindAsync(id);
            if (newspaperToDelete == null) return NotFound();

            _context.Newspapers.Remove(newspaperToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
