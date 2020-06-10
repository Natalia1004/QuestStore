using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class OwnedArtifactStudent
    {
        public int Id { get; set; }

        [Required]
        public int ArtifactId { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}
