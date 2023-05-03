using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IAcaoRepository
    {
        Task InsertAsync(Acao item);
        Task<Acao> UpdateAsync(Acao item);
        Task<Acao> DeleteAsync(Acao item);
        Task<bool> ExistAsync(int? id);
        Task<Acao> GetById(int id);
        Task<IEnumerable<Acao>> GetAll();
        Task InsertRangeAsync(IEnumerable<Acao> items);
        IQueryable<Acao> GetQueryable(Expression<Func<Acao, bool>>? query = null);
    }
}
