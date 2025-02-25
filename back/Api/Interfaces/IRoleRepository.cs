using back.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace back.Interfaces
{
    public interface IRoleRepository
    {
        public ActionResult<ResponseApi<object>> GetAllRoles();
    }
}
