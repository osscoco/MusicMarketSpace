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
        public required User User { get; set; }
        public required Guid SubChoiceId { get; set; }
        [JsonIgnore]
        public required SubChoice SubChoice { get; set; }
    }
}
