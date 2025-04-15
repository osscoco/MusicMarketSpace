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
                    UserId = user.UserId, 
                    Pseudo = user.Pseudo,
                    Mail = user.Mail,
                    ContactPhone = user.ContactPhone
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

        #region GetOneUserByEmail
        public async Task<ActionResult<UserResponse>> GetOneUserByEmail(string email)
        {
            var user = await _context.Users
                .Select(user => new UserResponse
                {
                    UserId = user.UserId,
                    Pseudo = user.Pseudo,
                    Mail = user.Mail,
                    ContactPhone = user.ContactPhone
                })
                .FirstOrDefaultAsync(u => u.Mail == email);

            return user!;
        }
        #endregion

        #region CreateOneUser
        public async Task<ActionResult<ResponseApi<object>>> CreateOneUser(UserCreateRequest user)
        {
            if (await this.UserExistsByPseudo(user.Pseudo))
            {
                return new ResponseApi<object>(false, null, "Le pseudo de l'utilisateur est déjà utilisé par un autre compte");
            }

            if (await this.UserExistsByEmail(user.Mail))
            {
                return new ResponseApi<object>(false, null, "L'email de l'utilisateur est déjà utilisé par un autre compte");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return new ResponseApi<object>(false, null, "Le mot de passe ne doit pas être vide");
            }

            User createdUser = new User
            {
                UserId = Guid.NewGuid(),
                Pseudo = user.Pseudo,
                Mail = user.Mail,
                PasswordHashed = HashString(user.Password)
            };

            _context.Users.Add(createdUser);
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Utilisateur créé avec succès");
        }
        #endregion

        #region UpdateOneUser
        public async Task<ActionResult<ResponseApi<object>>> UpdateOneUser(Guid userId, UserUpdateRequest user)
        {
            if (await this.UserExistsByUserId(userId) == false)
            {
                return new ResponseApi<object>(false, null, "L'utilisateur recherché n'existe pas");
            }

            if (await this.UserExistsByPseudo(user.Pseudo))
            {
                return new ResponseApi<object>(false, null, "Le nouveau pseudo de l'utilisateur existe déjà");
            }

            if (await this.UserExistsByEmail(user.Mail))
            {
                return new ResponseApi<object>(false, null, "Le nouvel email de l'utilisateur existe déjà");
            } 

            if (await this.UserExistsByContactPhone(user.ContactPhone))
            {
                return new ResponseApi<object>(false, null, "Le nouveau numéro de téléphone de l'utilisateur existe déjà");
            }

            if (userId == Guid.Empty)
            {
                return new ResponseApi<object>(false, null, "L'identifiant de l'utilisateur ne doit pas être vide");
            }

            if (string.IsNullOrWhiteSpace(user.Pseudo))
            {
                return new ResponseApi<object>(false, null, "Le pseudo de l'utilisateur ne doit pas être vide");
            }

            if (string.IsNullOrWhiteSpace(user.Mail))
            {
                return new ResponseApi<object>(false, null, "L'email de l'utilisateur ne doit pas être vide");
            }

            if (string.IsNullOrWhiteSpace(HashString(user.PasswordHashed)))
            {
                return new ResponseApi<object>(false, null, "Le Mot de Passe de l'utilisateur ne doit pas être vide");
            }

            if (await this.UserMatchByPasswordHashed(userId, HashString(user.PasswordHashed)) == false)
            {
                return new ResponseApi<object>(false, null, "Le Mot de Passe de l'utilisateur n'est pas valide");
            }

            if (string.IsNullOrWhiteSpace(user.ContactPhone))
            {
                return new ResponseApi<object>(false, null, "Le numéro de téléphone de l'utilisateur ne doit pas être vide");
            }

            User userUpdated = new User
            {
                UserId = userId,
                Pseudo = user.Pseudo,
                Mail = user.Mail,
                PasswordHashed = user.PasswordHashed,
                ContactPhone = user.ContactPhone
            };

            _context.Entry(userUpdated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Utilisateur modifié avec succès");
        }
        #endregion

        #region DeleteOneUser
        public async Task<ActionResult<ResponseApi<object>>> DeleteOneUser(Guid userId)
        {
            if (await this.UserExistsByUserId(userId) == false)
            {
                return new ResponseApi<object>(false, null, "L'utilisateur recherché n'existe pas");
            }

            _context.RemoveById<User>(userId);
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Utilisateur supprimé avec succès");
        }
        #endregion

        #region Functions personnalisées
        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Mail == email);
        }

        public async Task<bool> UserExistsByPseudo(string pseudo)
        {
            return await _context.Users.AnyAsync(u => u.Pseudo == pseudo);
        }

        public async Task<bool> UserExistsByContactPhone(string contactPhone)
        {
            return await _context.Users.AnyAsync(u => u.ContactPhone == contactPhone);
        }

        public async Task<bool> UserExistsByUserId(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }
        public async Task<bool> UserMatchByPasswordHashed(Guid userId, string passwordHashed)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId && u.PasswordHashed == passwordHashed);
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