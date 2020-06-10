using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Models
{
    public class OwnedQuestStudent
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public int StudentId { get; set; }
    }
}
