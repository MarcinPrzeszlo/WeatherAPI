using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.Entities
{
    public class WeatherApiDbContext: DbContext
    {
        private string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=WeatherAPI;Trusted_Connection=True;Integrated Security=True;";
        public  DbSet<Role> Roles { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
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
