using back.Dtos.Requests;
using back.Interfaces;
using back.Repositories;
using back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

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
        public async Task<ActionResult<ResponseApi<object>>> CreateRole([FromBody] RoleCreateRequest role)
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
    }
}