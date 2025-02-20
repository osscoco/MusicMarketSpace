using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Identity
{
    public class Choice
    {
        [Key]
        public Guid ChoiceId { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [JsonIgnore]
        public ICollection<SubChoice> SubChoices { get; set; } = new List<SubChoice>();
    }
}
