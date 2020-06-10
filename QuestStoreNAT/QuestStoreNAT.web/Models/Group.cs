using System.Collections.Generic;

namespace QuestStoreNAT.web.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public int CC_GroupWallet { get; set; }
        public List<Student> GroupStudents { get; set; }
        public List<Quest> GroupQuests { get; set; }
        public List<Artifact> GroupArtifacts { get; set; }
    }
}
