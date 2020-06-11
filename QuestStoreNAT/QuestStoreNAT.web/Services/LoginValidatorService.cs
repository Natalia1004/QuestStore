using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Services
{
    public class LoginValidatorService : ILoginValidatorService
    {
        private readonly CredentialsDAO _CredentialsDAO; 

        public LoginValidatorService()
        {
            _CredentialsDAO = new CredentialsDAO();
        }

        public bool IsValidLogin (Credentials enteredCredentials)
        {
            var fullDbCredentials = _CredentialsDAO.FindCredentials(enteredCredentials.Email);

            if (fullDbCredentials is null) return false;

            if (fullDbCredentials.Password == enteredCredentials.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
