using back.Dtos.Requests;
using back.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Interfaces
{
    public interface IUserRepository
    {
        public ActionResult<ResponseApi<object>> GetAllUsers();
        public Task<ActionResult<UserResponse>> GetOneUserByEmail(string email);
        public Task<ActionResult<ResponseApi<object>>> CreateOneUser(UserCreateRequest user);
        public Task<ActionResult<ResponseApi<object>>> UpdateOneUser(Guid userId, UserUpdateRequest user);
        public Task<bool> UserExistsByEmail(string email);
        public Task<bool> UserExistsByPseudo(string pseudo);
        public Task<bool> UserExistsByContactPhone(string contactPhone);
        public Task<bool> UserExistsByUserId(Guid userId);
        public Task<bool> UserMatchByPasswordHashed(Guid userId, string passwordHashed);
        public string HashString(string input);
    }
}