using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestStoreNAT.web.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Group> ClassroomGroups { get; set; }
        public List<Student> ClassroomStudents { get; set; }
        public List<Mentor> ClassroomMentors { get; set; }
    }
}
