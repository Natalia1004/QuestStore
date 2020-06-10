using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Mentor : IUser
    {
        public int Id { get; set; }

        public List<Classroom> MentorClassrooms { get; set; }

        [Required]
        public Credentials Credentials { get; set; }

        [Required(ErrorMessage = "Firstname Required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname Required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        public string Bio { get; set; }
    }
}
