using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using System;

namespace QuestStoreNAT.web.Services
{
    public class UserFinderService : IUserFinderService
    {
        public IUser RetrieveUser(Role role, string email)
        {
            switch (role)
            {
                case Role.Admin:
                    var adminDAO = new AdminDAO();
                    return adminDAO.FindAdmin(email);
                case Role.Mentor:
                    //TODO Mentor retrival
                    return new Mentor();
                default:
                    //TODO Student retrival
                    return new Student();
            }
        }
    }
}
