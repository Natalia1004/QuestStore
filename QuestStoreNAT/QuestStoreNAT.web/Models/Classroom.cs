using System.Collections.Generic;

namespace QuestStoreNAT.web.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Group> ClassroomGroups { get; set; }
        public List<Student> ClassroomStudents { get; set; }
        public List<Mentor> ClassroomMentors { get; set; }
    }
}
