namespace back.Dtos.Requests
{
    public class UserUpdateRequest
    {
        public required string Pseudo { get; set; }
        public required string Mail { get; set; }
        public required string PasswordHashed { get; set; }
        public string? ContactPhone { get; set; }

        public UserUpdateRequest() { }
    }
}