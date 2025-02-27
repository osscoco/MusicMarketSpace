using back.Dtos.Requests;
using back.Dtos.Responses;
using back.Interfaces;
using EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Identity;
using System.Data;

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
                    RoleId = role.RoleId,
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

        #region CreateOneRole
        public async Task<ActionResult<ResponseApi<object>>> CreateOneRole(RoleCreateRequest role)
        {
            if (await this.RoleExistsByName(role.Name))
            {
                return new ResponseApi<object>(false, null, "Le rôle existe déjà");
            }

            Role createdRole = new Role
            {
                RoleId = Guid.NewGuid(),
                Name = role.Name
            };

            _context.Roles.Add(createdRole);
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Rôle créé avec succès");
        }
        #endregion

        #region UpdateOneRole
        public async Task<ActionResult<ResponseApi<object>>> UpdateOneRole(Guid roleId, RoleUpdateRequest role)
        {
            if (await this.RoleExistsByRoleId(roleId) == false)
            {
                return new ResponseApi<object>(false, null, "Le rôle recherché n'existe pas");
            }

            if (await this.RoleExistsByName(role.Name))
            {
                return new ResponseApi<object>(false, null, "Le nouveau nom du rôle existe déjà");
            }

            if (string.IsNullOrWhiteSpace(role.Name))
            {
                return new ResponseApi<object>(false, null, "Le nom du rôle ne doit pas être vide");
            }

            Role roleUpdated = new Role 
            { 
                RoleId = roleId,
                Name = role.Name 
            };

            _context.Entry(roleUpdated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Rôle modifié avec succès");
        }
        #endregion

        #region DeleteOneRole
        public async Task<ActionResult<ResponseApi<object>>> DeleteOneRole(Guid roleId)
        {
            if (await this.RoleExistsByRoleId(roleId) == false)
            {
                return new ResponseApi<object>(false, null, "Le rôle recherché n'existe pas");
            }

            _context.RemoveById<Role>(roleId);
            await _context.SaveChangesAsync();

            return new ResponseApi<object>(true, null, "Rôle supprimé avec succès");
        }
        #endregion

        #region Functions personnalisées
        public async Task<bool> RoleExistsByName(string name)
        {
            return await _context.Roles.AnyAsync(u => u.Name == name);
        }

        public async Task<bool> RoleExistsByRoleId(Guid roleId)
        {
            return await _context.Roles.AnyAsync(u => u.RoleId == roleId);
        }        
        #endregion
    }
}
