using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Credentials
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string SALT { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
