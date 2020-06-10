using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Models
{
    public class OwnedArtifactGroup
    {
        public int Id { get; set; }
        public int ArtifactId { get; set; }
        public int GroupId { get; set; }
    }
}
