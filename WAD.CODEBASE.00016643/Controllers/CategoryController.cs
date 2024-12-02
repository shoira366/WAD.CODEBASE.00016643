using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.DTOs;
using WAD.CODEBASE._00016643.Models;

namespace WAD.CODEBASE._00016643.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await _context.Categories.ToArrayAsync();
        }

        // GET: api/Category/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var foundCategory = await _context.Categories.FindAsync(id);
            return foundCategory != null ? Ok(foundCategory) : NotFound();
        }

        // POST: api/Category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _context.Categories.AnyAsync(c => c.CategoryName == dto.Name))
            {
                return Conflict(new { message = "A category with this name already exists." });
            }

            var category = new Category { CategoryName = dto.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        // PUT: api/Category/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.CategoryId) return BadRequest();
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Category/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var foundCategory = await _context.Categories.FindAsync(id);
            if(foundCategory == null) return NotFound();

            _context.Categories.Remove(foundCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
