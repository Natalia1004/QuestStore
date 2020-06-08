using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models

{
    public class Student
    {
        public int StudentID { get; set; }

        public int ClassID { get; set; }

        public int GroupID { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        //add validaton data!
        public string Password { get; set; }

        [Required(ErrorMessage = "Firstname Required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname Required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        public int CC_Wallet { get; set; }
    }
}
