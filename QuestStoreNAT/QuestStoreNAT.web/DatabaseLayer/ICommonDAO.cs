using System;
using System.Collections.Generic;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public interface ICommonDAO
    {
        void DeleteRow(string tableName, int id);
    }
}
