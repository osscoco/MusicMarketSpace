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
    public class RoleRepository: IRoleRepository
    {
        #region Attributs
        private readonly AppDbContext _context;
        #endregion

        #region Constructeur
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAllRoles
        public ActionResult<ResponseApi<object>> GetAllRoles()
        {
            var roles = _context.Roles
                .Select(role => new RoleResponse
                {
                    Name = role.Name
                })
                .ToList();

            return new ResponseApi<object>
            {
                Success = true,
                Data = roles,
                Message = "Roles chargés !"
            };
        }
        #endregion
    }
}
