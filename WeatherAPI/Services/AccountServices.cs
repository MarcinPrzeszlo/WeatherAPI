using Microsoft.AspNetCore.Identity;
using WeatherAPI.Entities;
using WeatherAPI.Models;
using WeatherAPI.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.Services
{
    public interface IAccountServices
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        void DeleteUser(int userId, LoginDto dto);
    }


    public class AccountServices : IAccountServices
    {
        private readonly WeatherApiDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ConnectionValues _connectionValues;
        public AccountServices(WeatherApiDbContext dbContext, IPasswordHasher<User> passwordHasher, ConnectionValues connectionValues)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _connectionValues = connectionValues;
        }


        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = 2
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }


        public string GenerateJwt(LoginDto dto)
        {
            var user = VerifyLoginDto(dto);
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_connectionValues.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_connectionValues.ExpireDays);

            var token = new JwtSecurityToken
                (_connectionValues.JwtIssuer,
                _connectionValues.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void DeleteUser(int userId, LoginDto dto)
        {
            var user = VerifyLoginDto(dto);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        private User VerifyLoginDto(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                throw new BadRequestException(message: "Invailid email or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException(message: "Invailid email or password");

            return user;
        }
    }
}
