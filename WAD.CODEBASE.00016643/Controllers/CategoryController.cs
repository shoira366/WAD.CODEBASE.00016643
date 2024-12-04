using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.DTOs;
using WAD.CODEBASE._00016643.Models;
using WAD.CODEBASE._00016643.Repositories;

namespace WAD.CODEBASE._00016643.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _repository;
        // private readonly ApplicationDbContext _context;

        public CategoryController(CategoryRepository repository)
        {
           _repository = repository;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/Category/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var foundCategory = await _repository.GetByIdAsync(id);
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

            if(await _repository.CategoryExistsByNameAsync(dto.Name))
            {
                return Conflict(new { message = "A category with this name already exists" });
            }

            var category = new Category { CategoryName = dto.Name };
            await _repository.CreateAsync(category);

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        // PUT: api/Category/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if(await _repository.CategoryExistsByNameAsync(category.CategoryName))
            {
                return Conflict(new { message = "A category with this name already exists" });
            }

            if (id != category.CategoryId) return BadRequest();
            await _repository.UpdateAsync(category);
            return NoContent();
        }

        // DELETE: api/Category/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var foundCategory = await _repository.GetByIdAsync(id);
            if(foundCategory == null) return NotFound();

            await _repository.DeleteAsync(foundCategory);
            return NoContent();
        }
    }
}
