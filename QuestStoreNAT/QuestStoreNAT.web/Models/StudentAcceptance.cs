using System;
using System.Collections.Generic;

namespace QuestStoreNAT.web.Models
{
    public class StudentAcceptance
    {
        public int ID { get; set; }
        public int studentID { get; set; }
        public string artifactName { get; set; }
        public int amountOfAcceptance { get; set; }
        public bool acceptance { get; set; }
    }
}
