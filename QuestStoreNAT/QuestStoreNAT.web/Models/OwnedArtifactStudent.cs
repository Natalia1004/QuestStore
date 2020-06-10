using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Models
{
    public class OwnedArtifactStudent
    {
        public int Id { get; set; }
        public int ArtifactId { get; set; }
        public int StudentId { get; set; }
    }
}
