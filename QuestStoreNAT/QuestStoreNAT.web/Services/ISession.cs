using QuestStoreNAT.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Services
{
    public interface ISession
    {
        public IUser LoggedUser { get; set; }
        public Role LoggedUserRole { get; set; }
    }
}
