using back.Interfaces;
using back.Repositories;
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
        public UserController(IUserRepository userRepository)
        {
            _userRepository = (UserRepository)userRepository;
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
                var statusCode = StatusCode(500, new { error = ex.Message });

                return new ResponseApi<object>
                {
                    Success = true,
                    Data = statusCode,
                    Message = "Utilisateurs chargés !"
                };
            }
        }
    }
}