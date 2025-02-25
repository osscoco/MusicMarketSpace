namespace back.Dtos.Responses
{
    public class RoleResponse
    {
        public Guid RoleId { get; set; }
        public required string Name { get; set; }
        public RoleResponse() { }
    }
}
