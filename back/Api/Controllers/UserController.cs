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
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly TranslateException _translateException;
        public UserController(IUserRepository userRepository, TranslateException translateException)
        {
            _userRepository = (UserRepository)userRepository;
            _translateException = translateException;
        }

        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ResponseApi<object>> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
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

        // POST: api/User
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseApi<object>>> CreateOneUser([FromBody] UserCreateRequest user)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseApi<object>(false, null, "Les données fournies sont invalides");
            }

            try
            {
                return await _userRepository.CreateOneUser(user);
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

        // PUT: api/User/{userId}
        [AllowAnonymous]
        [HttpPut("{userId}")]
        public async Task<ActionResult<ResponseApi<object>>> UpdateOneUser(Guid userId, [FromBody] UserUpdateRequest updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseApi<object>(false, null, "Les données fournies sont invalides");
            }

            try
            {
                return await _userRepository.UpdateOneUser(userId, updatedUser);
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

        // DELETE: api/User/{userId}
        [AllowAnonymous]
        [HttpDelete("{userId}")]
        public async Task<ActionResult<ResponseApi<object>>> DeleteOneUser(Guid userId)
        {
            try
            {
                return await _userRepository.DeleteOneUser(userId);
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