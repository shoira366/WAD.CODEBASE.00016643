using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.Models;

namespace WAD.CODEBASE._00016643.Repositories
{
    public class CategoryRepository: IRepository<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category entity)
        {
            var category = await _context.Categories.FindAsync(entity.CategoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateAsync(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsByNameAsync(string categoryName)
        {
            return await _context.Categories
                                 .AnyAsync(c => c.CategoryName == categoryName);
        }
    }
}
