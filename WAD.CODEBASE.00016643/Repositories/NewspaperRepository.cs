using Microsoft.EntityFrameworkCore;
using WAD.CODEBASE._00016643.Data;
using WAD.CODEBASE._00016643.Models;

namespace WAD.CODEBASE._00016643.Repositories
{
    public class NewspaperRepository: IRepository<Newspaper>
    {
        private readonly ApplicationDbContext _context;

        public NewspaperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Newspaper entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Newspaper entity)
        {
            var newspaper = await _context.Newspapers.FindAsync(entity.NewspaperId);
            if (newspaper != null)
            {
                _context.Newspapers.Remove(newspaper);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Newspaper>> GetAllAsync()
        {
            return await _context.Newspapers.Include(n => n.Articles).ToListAsync();
        }

        public async Task<Newspaper> GetByIdAsync(int id)
        {
            return await _context.Newspapers.Include(n => n.Articles).FirstOrDefaultAsync(n => n.NewspaperId == id);
        }

        public async Task UpdateAsync(Newspaper entity)
        {
            _context.Newspapers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Newspaper> GetNewspaper(int id)
        {
            return await _context.Newspapers.FindAsync(id);
        }
    }
}
