using back.Dtos.Requests;
using back.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using Models.Common;
using System.Collections;

namespace back.Interfaces
{
    public interface IAuthRepository
    {
        public Task<ActionResult<ResponseApi<object>>> Login(LoginAuthRequest loginAuth);
        public string HashString(string input);
        public Task<bool> UserExistsByEmailAndPassword(string email, string passwordHashed);
        public ArrayList GetUserAndBearerTokenByUser(UserResponse user);
    }
}
