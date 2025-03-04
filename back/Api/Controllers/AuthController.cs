using back.Dtos.Requests;
using back.Interfaces;
using back.Repositories;
using back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Attributs
        private readonly AuthRepository _authRepository;
        private readonly TranslateException _translateException;
        #endregion

        #region Constructeur
        public AuthController(IAuthRepository authRepository, TranslateException translateException)
        {
            _authRepository = (AuthRepository)authRepository;
            _translateException = translateException;
        }
        #endregion

        #region Login
        // POST: api/Auth
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ResponseApi<object>>> Login([FromBody] LoginAuthRequest loginAuth)
        {
            try
            {
                return await _authRepository.Login(loginAuth);
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
