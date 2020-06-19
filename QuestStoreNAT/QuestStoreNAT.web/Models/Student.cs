using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Student : IUser
    {
        public int Id { get; set; }

        [Required]
        public int ClassID { get; set; }

        public int GroupID { get; set; }

        [Required]
        public int CredentialId { get; set; }

        [Required(ErrorMessage = "First name required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public int Wallet { get; set; }

        [Required]
        public int OverallWalletLevel { get; set; }

        public List<Quest> StudentQuests { get; set; }
        public List<Artifact> StudentArtifacts { get; set; }
    }
}
