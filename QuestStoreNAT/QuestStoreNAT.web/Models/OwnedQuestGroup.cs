using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Models
{
    public class OwnedQuestGroup
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public int GroupId { get; set; }
    }
}
