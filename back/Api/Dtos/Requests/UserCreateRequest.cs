using back.Validators;

namespace back.Dtos.Requests
{
    public class UserCreateRequest
    {
        public required string Pseudo { get; set; }
        [CustomEmailValidator]
        public required string Mail { get; set; }
        public required string Password { get; set; }
        public string? ContactPhone { get; set; }
        public bool IsSSO { get; set; }
        public Guid RoleId { get; set; }        
    }
}
