using QuestStoreNAT.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Services
{
    public class Session : ISession
    {
        public IUser LoggedUser { get; set; }
        public Role LoggedUserRole { get; set; }
    }
}
