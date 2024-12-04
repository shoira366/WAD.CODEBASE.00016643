using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.Models;
using WAD.CODEBASE._00016643.Repositories;

namespace WAD.CODEBASE._00016643.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewspaperController : ControllerBase
    {
        private readonly NewspaperRepository _repository;   
        // private readonly ApplicationDbContext _context;

        public NewspaperController(NewspaperRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Newspapaer
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var newspapers = await _repository.GetAllAsync();
            return Ok(newspapers);
        }

        // GET: api/Newspaper/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Newspaper), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var foundNewspaper = await _repository.GetByIdAsync(id);
            return foundNewspaper != null ? Ok(foundNewspaper) : NotFound();
        }

        [HttpGet("ignore/{id}")]
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task<IActionResult> GetNewspaper(int id)
        {
            var foundNewspaper = await _repository.GetNewspaper(id);
            return foundNewspaper != null ? Ok(foundNewspaper) : NotFound();
        }

        // POST: api/Newspaper
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Newspaper newspaper)
        {
            await _repository.CreateAsync(newspaper);

            return CreatedAtAction(nameof(GetNewspaper), new { id = newspaper.NewspaperId }, newspaper);
        }

        // PUT: api/Newspaper/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Newspaper newspaper)
        {
            if (id != newspaper.NewspaperId) return BadRequest();
            await _repository.UpdateAsync(newspaper);

            return NoContent();
        }

        // DELETE: api/Newspaper/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var newspaperToDelete = await _repository.GetNewspaper(id);
            if (newspaperToDelete == null) return NotFound();

            await _repository.DeleteAsync(newspaperToDelete);
            return NoContent();
        }
    }
}
