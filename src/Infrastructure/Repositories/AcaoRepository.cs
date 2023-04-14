using Domain;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AcaoRepository : IAcaoRepository
    {
        private MercVistaContext _context;
        private DbSet<Acao> _dbSet;

        public AcaoRepository(MercVistaContext context)
        {
            _context = context;
            _dbSet = context.Set<Acao>();
        }

        public async Task<Acao> DeleteAsync(Acao item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> ExistAsync(int? id)
         => await _dbSet.AsNoTracking().AnyAsync(p => p.Id.Equals(id));

        public async Task<IEnumerable<Acao>> GetAll()
         => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<Acao?> GetById(int? id)
         => await _dbSet.AsNoTracking().Where(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<Acao> InsertAsync(Acao item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Acao>> InsertRangeAsync(IEnumerable<Acao> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<Acao> UpdateAsync(Acao item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
