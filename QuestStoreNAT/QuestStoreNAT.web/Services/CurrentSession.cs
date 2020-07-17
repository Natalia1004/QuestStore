using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public class CurrentSession : ICurrentSession
    {
        //TODO - clean this up by 19.07
        private static readonly StudentDAO StudentDao = new StudentDAO();
        public IUser LoggedUser { get; set; } = StudentDao.FindOneRecordBy(26);
        public Role LoggedUserRole { get; set; } = Role.Student;
    }
}
