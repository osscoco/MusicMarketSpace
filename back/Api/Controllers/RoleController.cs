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
        #region Attributs
        private readonly RoleRepository _roleRepository;
        private readonly TranslateException _translateException;
        #endregion

        #region Constructeur
        public RoleController(IRoleRepository roleRepository, TranslateException translateException)
        {
            _roleRepository = (RoleRepository)roleRepository;
            _translateException = translateException;
        }
        #endregion

        #region GetAllRoles
        // GET: api/Roles
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
        #endregion

        #region CreateOneRole
        // POST: api/Role
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
        #endregion

        #region UpdateOneRole
        // PUT: api/Role/{roleId}
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
        #endregion

        #region DeleteOneRole
        // DELETE: api/Role/{roleId}
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
        #endregion
    }
}