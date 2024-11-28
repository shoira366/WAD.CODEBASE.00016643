using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.Models;

namespace WAD.CODEBASE._00016643.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Article
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetAll()
        {
            return await _context.Articles.Include(a => a.Newspaper).Include(a => a.Category).ToListAsync();
        }

        // GET: api/Article/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Article), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var foundArticle = await _context.Articles
                .Include(a => a.Newspaper)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.ArticleId == id);

            return foundArticle != null ? Ok(foundArticle) : NotFound();
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetArticle(int id)
        {
            var foundArticle = await _context.Articles.FindAsync(id);
            return foundArticle != null ? Ok(foundArticle) : NotFound();
        }

        // POST: api/Article
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Article article)
        {
            var newspaper = await _context.Newspapers.FindAsync(article.NewspaperId);
            if (newspaper == null)
            {
                return NotFound(new { error = "Newspaper not found." });
            }

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = article.ArticleId }, article);
        }

        // PUT: api/Article/2
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Article article)
        {
            if (id != article.ArticleId) return BadRequest();
            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Article/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var articleToDelete = await _context.Articles.FindAsync(id);
            if (articleToDelete == null) return NotFound();

            _context.Articles.Remove(articleToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
