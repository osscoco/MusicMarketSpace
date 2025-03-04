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
    public class AuthController : ControllerBase
    {
        #region Attributs
        private readonly AuthRepository _authRepository;
        private readonly TranslateException _translateException;
        private readonly Token _token;
        #endregion

        #region Constructeur
        public AuthController(IAuthRepository authRepository, TranslateException translateException, Token token)
        {
            _authRepository = (AuthRepository)authRepository;
            _translateException = translateException;
            _token = token;
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

        #region Logout
        [HttpGet("logout")]
        public ResponseApi<object> Logout()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return new ResponseApi<object>
                    {
                        Success = false,
                        Data = BadRequest("Aucun jeton fourni"),
                        Message = "Aucun jeton fourni"
                    };
                }

                _token.RevokeToken(token);

                return new ResponseApi<object>
                {
                    Success = true,
                    Data = Ok("Vous êtes déconnecté"),
                    Message = "Vous êtes déconnecté"
                };
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

        #region GetAuthMe
        [HttpGet("getAuthMe")]
        public ResponseApi<object> GetAuthMe()
        {
            try
            {
                var user = HttpContext.User;

                if (!user.Identity.IsAuthenticated)
                {
                    return new ResponseApi<object>
                    {
                        Success = false,
                        Data = Unauthorized(new { message = "Vous n'êtes pas authentifié." }),
                        Message = "Vous n'êtes pas authentifié"
                    };
                }

                var emailUserInfo = "";

                foreach (var (item, index) in user.Claims.Select((value, i) => (value, i)))
                {
                    if (index == 0)
                        emailUserInfo = item.Value;
                }

                return new ResponseApi<object>
                {
                    Success = true,
                    Data = Ok(emailUserInfo),
                    Message = "Vous êtes authentifié"
                };
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
