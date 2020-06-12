using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public interface ILoginValidatorService
    {
        bool IsValidLogin(Credentials enteredCredentials);
        public Role UserRole { get; set; }
    }
}
