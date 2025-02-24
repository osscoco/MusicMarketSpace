using back.Dtos.Requests;
using back.Dtos.Responses;
using back.Interfaces;
using EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Identity;
using System.Security.Cryptography;
using System.Text;

namespace back.Repositories
{
    public class UserRepository: IUserRepository
    {
        #region Attributs
        private readonly AppDbContext _context;
        #endregion

        #region Constructeur
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllUsers
        public ActionResult<ResponseApi<object>> GetAllUsers()
        {
            var users = _context.Users
                .Select(user => new UserResponse
                {
                    Pseudo = user.Pseudo,
                    Mail = user.Mail,
                    ContactPhone = user.ContactPhone,
                    IsSSO = user.IsSSO,
                    RoleId = user.RoleId
                })
                .ToList();

            return new ResponseApi<object>
            {
                Success = true,
                Data = users,
                Message = "Utilisateurs chargés !"
            };
        }
        #endregion

        #region CreateOneUser
        public async Task<ActionResult<ResponseApi<object>>> CreateOneUser(UserCreateRequest user)
        {
            if (await this.UserExistsByEmail(user.Mail))
            {
                return new ResponseApi<object>(false, null, "L'utilisateur existe déjà");
            }

            User createdUser = new User
            {
                Pseudo = user.Pseudo,
                Mail = user.Mail,
                PasswordHashed = HashString(user.Password),
                ContactPhone = user.ContactPhone,
                IsSSO = user.IsSSO,
                RoleId = user.RoleId
            };

            _context.Users.Add(createdUser);
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Utilisateur créé avec succès");
        }
        #endregion

        #region Functions personnalisées
        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Mail == email);
        }

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
        #endregion
    }
}
