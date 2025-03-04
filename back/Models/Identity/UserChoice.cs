using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Identity
{
    public class UserChoice
    {
        [Key]
        public Guid UserChoiceId { get; set; }
        public required Guid UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public required Guid SubChoiceId { get; set; }
        [JsonIgnore]
        public SubChoice? SubChoice { get; set; }

        public UserChoice() { }
    }
}
