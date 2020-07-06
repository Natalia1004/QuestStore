using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public class UserFinderService : IUserFinderService
    {
        public IUser RetrieveUser(Role role, int credentialId)
        {
            switch (role)
            {
                case Role.Admin:
                    var adminDAO = new AdminDAO();
                    return adminDAO.FindOneRecordByCredentialId(credentialId);
                case Role.Mentor:
                    var mentorDAO = new MentorDAO();
                    return mentorDAO.FindOneRecordByCredentialId(credentialId);
                default:
                    var studentDAO = new StudentDAO();
                    return studentDAO.FindOneRecordByCredentialId(credentialId);
            }
        }

        public IUser RetrieveUser(Role role, string email)
        {
            switch (role)
            {
                case Role.Admin:
                    var adminDAO = new AdminDAO();
                    return adminDAO.FindOneRecordBy(email);
                case Role.Mentor:
                    var mentorDAO = new MentorDAO();
                    return mentorDAO.FindOneRecordBy(email);
                default:
                    var studentDAO = new StudentDAO();
                    return studentDAO.FindOneRecordBy(email);
            }
        }
    }
}
