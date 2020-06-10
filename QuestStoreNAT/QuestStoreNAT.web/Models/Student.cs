using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Student : IUser
    {
        public int Id { get; set; }

        public int ClassID { get; set; }

        public int GroupID { get; set; }
        public List<Quest> StudentQuests { get; set; }
        public List<Artifact> StudentArtifacts { get; set; }

        [Required]
        public Credentials Credentials { get; set; }

        [Required(ErrorMessage = "Firstname required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        public int Wallet { get; set; }
        public int OverallWalletLevel { get; set; }
    }
}
