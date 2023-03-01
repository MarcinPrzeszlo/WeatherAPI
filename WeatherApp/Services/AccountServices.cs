using Microsoft.AspNetCore.Identity;
using WeatherApp.Entities;
using WeatherApp.Models;
using WeatherApp.Exceptions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace WeatherApp.Services
{
    public interface IAccountService
    {
        bool Register(RegisterDto dto);
        string GenerateJwt(LoginDto dto);
    }

    public class AccountServices : IAccountService
    {
        private readonly WeatherAppDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ConnectionValues _connectionValues;

        public AccountServices(WeatherAppDbContext dbContext, IPasswordHasher<User> passwordHasher, ConnectionValues connectionValues)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _connectionValues = connectionValues;
        }

        public bool Register(RegisterDto dto)
        {
            var userExist = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (userExist != null)
            {
                return false;
            }

            var newUser = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                RoleId = 1
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash= hashedPassword;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return true;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
            {
                return "skucha";
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return "skucha";
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_connectionValues.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires =  DateTime.Now.AddDays(_connectionValues.JwtExpireDays);

            var token = new JwtSecurityToken(_connectionValues.JwtIssuer, 
               _connectionValues.JwtIssuer,
               claims,
               expires: expires,
               signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
