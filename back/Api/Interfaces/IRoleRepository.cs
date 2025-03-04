using back.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Interfaces
{
    public interface IRoleRepository
    {
        public ActionResult<ResponseApi<object>> GetAllRoles();
        public Task<ActionResult<ResponseApi<object>>> CreateOneRole(RoleCreateRequest role);
        public Task<ActionResult<ResponseApi<object>>> UpdateOneRole(Guid roleId, RoleUpdateRequest role);
        public Task<ActionResult<ResponseApi<object>>> DeleteOneRole(Guid roleId);
        public Task<bool> RoleExistsByName(string name);
        public Task<bool> RoleExistsByRoleId(Guid roleId);
    }
}
