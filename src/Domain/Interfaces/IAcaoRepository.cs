namespace Domain.Interfaces
{
    public interface IAcaoRepository
    {
        Task<Acao> InsertAsync(Acao item);
        Task<Acao> UpdateAsync(Acao item);
        Task<Acao> DeleteAsync(Acao item);
        Task<bool> ExistAsync(int? id);
        Task<Acao> GetById(int? id);
        Task<IEnumerable<Acao>> GetAll();
        Task<IEnumerable<Acao>> InsertRangeAsync(IEnumerable<Acao> items);
    }
}
