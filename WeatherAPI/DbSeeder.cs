using WeatherAPI.Entities;

namespace WeatherAPI
{
    public class DbSeeder
    {
        private readonly WeatherApiDbContext _dbContext;

        public DbSeeder(WeatherApiDbContext dbContext)
        {
            _dbContext = dbContext;          
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "User"
                }
            };

            return roles;
        }
    }
}
