using Microsoft.EntityFrameworkCore;
namespace WeatherApp.Entities
{
    public class WeatherAppDbContext: DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=WeatherApplicationDataBase;Trusted_Connection=True;";
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


    }
}
