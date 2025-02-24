using back.Dtos.Responses;
using back.Interfaces;
using EFCore;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

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
    }
}
