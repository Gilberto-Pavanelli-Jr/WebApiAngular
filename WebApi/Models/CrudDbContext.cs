using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Crud> Cruds { get; set; }
    }
}
