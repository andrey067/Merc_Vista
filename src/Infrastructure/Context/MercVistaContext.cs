using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class MercVistaContext : DbContext
    {
        protected MercVistaContext() { }
        public MercVistaContext(DbContextOptions<MercVistaContext> options)
         : base(options) { }

        public DbSet<Acao> Acaoes { get; set; }
    }
}