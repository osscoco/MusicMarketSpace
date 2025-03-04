using back.Validators;

namespace back.Dtos.Requests
{
    public class LoginAuthRequest
    {
        [CustomEmailValidator]
        public required string Mail { get; set; }
        public required string Password { get; set; }

        public LoginAuthRequest() { }
    }
}
