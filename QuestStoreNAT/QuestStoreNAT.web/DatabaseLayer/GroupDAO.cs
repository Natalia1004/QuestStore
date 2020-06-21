using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class GroupDAO : DBAbstractRecord<Group>
    {
        public override string DBTableName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override Group ProvideOneRecord( NpgsqlDataReader reader )
        {
            throw new NotImplementedException();
        }

        public override string ProvideQueryStringToAdd( Group recordToAdd )
        {
            throw new NotImplementedException();
        }

        public override string ProvideQueryStringToUpdate( Group recordToUpdate )
        {
            throw new NotImplementedException();
        }
    }
}
