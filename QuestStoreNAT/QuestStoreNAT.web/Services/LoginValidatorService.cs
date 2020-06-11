using QuestStoreNAT.web.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStoreNAT.web.Services
{
    public class LoginValidatorService
    {
        private readonly CredentialsDAO _CredentialsDAO; 

        public LoginValidatorService()
        {
            _CredentialsDAO = new CredentialsDAO();
        }

        public bool VerifyLogin (string email, string password)
        {
            var fullCredentials = _CredentialsDAO.FindCredentials(email);

            if (fullCredentials.Password == password)
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
