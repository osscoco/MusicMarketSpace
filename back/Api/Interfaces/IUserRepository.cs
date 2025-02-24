using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Interfaces
{
    public interface IUserRepository
    {
        public ActionResult<ResponseApi<object>> GetAllUsers();
    }
}
