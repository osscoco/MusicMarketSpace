using back.Dtos.Requests;
using back.Dtos.Responses;
using back.Interfaces;
using back.Repositories;
using back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Identity;

namespace back.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;
        private readonly TranslateException _translateException;
        public RoleController(IRoleRepository roleRepository, TranslateException translateException)
        {
            _roleRepository = (RoleRepository)roleRepository;
            _translateException = translateException;
        }

        // GET: api/Roles
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ResponseApi<object>> GetAllRoles()
        {
            try
            {
                return _roleRepository.GetAllRoles();
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>
                {
                    Success = false,
                    Data = StatusCode(500, new { error = this._translateException.TranslateExceptionEnToFr(ex) }),
                    Message = "" + this._translateException.TranslateExceptionEnToFr(ex)
                };
            }
        }

        // POST: api/Role
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseApi<object>>> CreateOneRole([FromBody] RoleCreateRequest role)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseApi<object>(false, null, "Les données fournies sont invalides");
            }

            try
            {
                return await _roleRepository.CreateOneRole(role);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>
                {
                    Success = false,
                    Data = StatusCode(500, new { error = this._translateException.TranslateExceptionEnToFr(ex) }),
                    Message = "" + this._translateException.TranslateExceptionEnToFr(ex)
                };
            }
        }

        // PUT: api/Role/{roleId}
        [AllowAnonymous]
        [HttpPut("{roleId}")]
        public async Task<ActionResult<ResponseApi<object>>> UpdateOneRole(Guid roleId, [FromBody] RoleUpdateRequest updatedRole)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseApi<object>(false, null, "Les données fournies sont invalides");
            }

            try
            {
                return await _roleRepository.UpdateOneRole(roleId, updatedRole);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>
                {
                    Success = false,
                    Data = StatusCode(500, new { error = this._translateException.TranslateExceptionEnToFr(ex) }),
                    Message = "" + this._translateException.TranslateExceptionEnToFr(ex)
                };
            }
        }

        // DELETE: api/User/{roleId}
        [AllowAnonymous]
        [HttpDelete("{roleId}")]
        public async Task<ActionResult<ResponseApi<object>>> DeleteOneRole(Guid roleId)
        {
            try
            {
                return await _roleRepository.DeleteOneRole(roleId);
            }
            catch (Exception ex)
            {
                return new ResponseApi<object>
                {
                    Success = false,
                    Data = StatusCode(500, new { error = this._translateException.TranslateExceptionEnToFr(ex) }),
                    Message = "" + this._translateException.TranslateExceptionEnToFr(ex)
                };
            }
        }
    }
}