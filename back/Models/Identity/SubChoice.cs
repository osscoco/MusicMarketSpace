using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Identity
{
    public class SubChoice
    {
        [Key]
        public Guid SubChoiceId { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        public Guid ChoiceId { get; set; }
        [JsonIgnore]
        public Choice Choice { get; set; }
    }
}
