using back.Dtos.Requests;
using back.Dtos.Responses;
using back.Interfaces;
using EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.Identity;
using System.Collections;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace back.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        #region Attributs
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructeur
        public AuthRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #endregion

        #region Login
        public async Task<ActionResult<ResponseApi<object>>> Login(LoginAuthRequest loginAuth)
        {
            var passwordHash = HashString(loginAuth.Password);

            if (await this.UserExistsByEmailAndPassword(loginAuth.Mail, passwordHash))
            {
                UserResponse userResponse = await _context.Users
                .Where(user => user.Mail == loginAuth.Mail)
                .Select(user => new UserResponse
                {
                    UserId = user.UserId,
                    Pseudo = user.Pseudo,
                    Mail = user.Mail,
                    ContactPhone = user.ContactPhone,
                    IsSSO = user.IsSSO,
                    RoleId = user.RoleId
                })
                .FirstAsync();

                ArrayList userAndbearerToken = this.GetUserAndBearerTokenByUser(userResponse);

                return new ResponseApi<object>
                {
                    Success = true,
                    Data = userAndbearerToken,
                    Message = "Vous êtes connecté !"
                };
            }

            return new ResponseApi<object>
            {
                Success = false,
                Data = null,
                Message = "Echec de l'authentification !"
            };
        }
        #endregion

        #region Functions personnalisées
        public string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public async Task<bool> UserExistsByEmailAndPassword(string email, string passwordHashed)
        {
            return await _context.Users.AnyAsync(u => u.Mail == email && u.PasswordHashed == passwordHashed);
        }

        public ArrayList GetUserAndBearerTokenByUser(UserResponse user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Mail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: signin);
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            ArrayList arraylist = new ArrayList();
            arraylist.Add(user);
            arraylist.Add(tokenValue);

            return arraylist;
        }
        #endregion
    }
}
