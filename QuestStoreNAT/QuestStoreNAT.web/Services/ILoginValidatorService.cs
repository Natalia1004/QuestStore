using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public interface ILoginValidatorService
    {
        public Role GetUserRole();
        public int GetUserCredentialId();
        public bool IsValidLogin(Credentials enteredCredentials);
        public bool IsValidPasswordHASH(Credentials enteredCredentials);
    }
}
