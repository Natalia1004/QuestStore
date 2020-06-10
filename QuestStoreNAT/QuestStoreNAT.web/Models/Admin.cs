using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Admin : IUser
    {
        public int Id { get; set; }

        [Required]
        public Credentials Credentials { get; set; }

        [Required(ErrorMessage = "Firstname required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
