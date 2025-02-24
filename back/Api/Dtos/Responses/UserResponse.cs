namespace back.Dtos.Responses
{
    public class UserResponse
    {
        public required string Pseudo { get; set; }
        public required string Mail { get; set; }
        public string? ContactPhone { get; set; }
        public bool IsSSO { get; set; }
        public Guid RoleId { get; set; }

        public UserResponse() { }
    }
}
