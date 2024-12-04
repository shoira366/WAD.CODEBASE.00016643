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
    public class ArticleController : ControllerBase
    {
        private readonly IRepository<Article> _repository;
        private readonly NewspaperRepository _newspaperRepository;
        // private readonly ApplicationDbContext _context;

        public ArticleController(IRepository<Article> repository, NewspaperRepository newspaperRepository)
        {
            _repository = repository;
            _newspaperRepository = newspaperRepository;
        }


        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var articles = await _repository.GetAllAsync();
            return Ok(articles);
        }

        // GET: api/Article/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Article), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var foundArticle = await _repository.GetByIdAsync(id);

            return foundArticle != null ? Ok(foundArticle) : NotFound();
        }

        /*[HttpGet("ignore/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetArticle(int id)
        {
            var foundArticle = await _repository.GetArticle(id);
            return foundArticle != null ? Ok(foundArticle) : NotFound();
        }*/

        // POST: api/Article
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Article article)
        {
            var newspaper = await _newspaperRepository.GetNewspaper(article.NewspaperId);
            if (newspaper == null)
            {
                return NotFound(new { error = "Newspaper not found." });
            }

           await _repository.CreateAsync(article);

            return CreatedAtAction(nameof(GetById), new { id = article.ArticleId }, article);
        }

        // PUT: api/Article/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Article article)
        {
            if (id != article.ArticleId) return BadRequest();
            await _repository.UpdateAsync(article);

            return NoContent();
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var articleToDelete = await _repository.GetByIdAsync(id);
            if (articleToDelete == null) return NotFound();

            await _repository.DeleteAsync(articleToDelete);
            return NoContent();
        }

    }
}
