using WeatherApp.Entities;

namespace WeatherApp
{
    public class DbSeeder
    {
        private readonly WeatherAppDbContext _dbContext;

        public DbSeeder(WeatherAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                //if (!_dbContext.Users.Any())
                //{
                //    var users = GetUsers();
                //    _dbContext.Users.AddRange(users);
                //    _dbContext.SaveChanges();
                //}
            }

        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        //private IEnumerable<User> GetUsers()
        //{
        //    var users = new List<User>()
        //    {
        //        new User()
        //        {
        //            Email = "user1@user1.pl",
        //            Name = "user1",
        //            RoleId = 1
        //        },
        //        new User()
        //        {
        //            Email = "admin1@admin1.pl",
        //            Name = "admin1",
        //            RoleId = 2
        //        }
        //    };
        //    return users;
        //}
    }
}
