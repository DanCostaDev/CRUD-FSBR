using CRUD_FSBR.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_FSBR.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produto { get; set; }
    }
}
