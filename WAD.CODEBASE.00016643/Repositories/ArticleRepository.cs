using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.Models;

namespace WAD.CODEBASE._00016643.Repositories
{
    public class ArticleRepository: IRepository<Article>
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Article entity)
        {
            await _context.Articles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Article entity)
        {
            var article = await _context.Articles.FindAsync(entity.ArticleId);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles.ToArrayAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task UpdateAsync(Article entity)
        {
            _context.Articles.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
