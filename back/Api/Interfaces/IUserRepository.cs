using back.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Interfaces
{
    public interface IUserRepository
    {
        public ActionResult<ResponseApi<object>> GetAllUsers();
        public Task<ActionResult<ResponseApi<object>>> CreateOneUser(UserCreateRequest user);
        public Task<bool> UserExistsByEmail(string email);
        public string HashString(string input);
    }
}
