using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        [Required]
        public string Name { get; set; }

        public int GroupWallet { get; set; }

        public List<Student> GroupStudents { get; set; }
        public List<Quest> GroupQuests { get; set; }

        public Group()
        {
            //GroupWallet = CalculateGroupWallet();
        }

        /* ---ponieważ na razie liczymy wallet dla grupy trochę inaczej tymczasowo to zakomentowałem ----
        public int CalculateGroupWallet()
        {
            return GroupStudents.Sum(student => student.Wallet);
        }
        */
    }
}
