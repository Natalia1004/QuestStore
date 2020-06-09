using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Student : IUser
    {
        public int StudentID { get; set; }

        public int ClassID { get; set; }

        public int GroupID { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [PasswordPropertyText]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Firstname required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        public int CC_Wallet { get; set; }
    }
}
