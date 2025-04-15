namespace back.Dtos.Responses
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public required string Pseudo { get; set; }
        public required string Mail { get; set; }
        public string? ContactPhone { get; set; }

        public UserResponse() { }
    }
}