using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class OwnedQuestStudent
    {
        public int Id { get; set; }

        [Required]
        public int QuestId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CompletionStatus { get; set; }

        public virtual string Name { get; set; }
        public virtual int Cost { get; set; }
    }
}
