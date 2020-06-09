using System;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public interface IArtifactDAO
    {
        List<Artifact> GetAllRows();
    }
}
