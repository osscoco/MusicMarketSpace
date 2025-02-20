using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Identity
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}