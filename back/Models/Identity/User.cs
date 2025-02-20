using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Identity
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public required string Pseudo { get; set; }

        [MaxLength(255)]
        public required string Mail { get; set; }

        public required string PasswordHashed { get; set; }
        [MaxLength(10)]
        public string? ContactPhone { get; set; }
        public bool IsSSO { get; set; }
        public Guid RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public ICollection<UserChoice> Choices { get; set; } = new List<UserChoice>();

        public User() { }
    }
}