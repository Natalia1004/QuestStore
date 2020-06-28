using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public interface ILoginValidatorService
    {
        public Role GetUserRole();
        bool IsValidLogin(Credentials enteredCredentials);
        bool IsValidPasswordHASH(Credentials enteredCredentials);
    }
}
