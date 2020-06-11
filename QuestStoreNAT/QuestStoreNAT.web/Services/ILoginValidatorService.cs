using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public interface ILoginValidatorService
    {
        bool IsValidLogin(Credentials enteredCredentials);
    }
}
