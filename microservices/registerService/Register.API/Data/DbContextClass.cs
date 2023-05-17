using Microsoft.EntityFrameworkCore;
using Register.API.Models;

namespace Register.API.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
        }
        public DbSet<Cliente> Cliente
        {
            get;
            set;
        }
    }
}
