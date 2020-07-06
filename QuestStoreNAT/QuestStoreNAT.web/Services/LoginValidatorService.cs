﻿using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Services
{
    public class LoginValidatorService : ILoginValidatorService
    {
        private readonly CredentialsDAO _CredentialsDAO;
        private Role UserRole { get; set; }
        private int CredentialId { get; set; }

        public LoginValidatorService()
        {
            _CredentialsDAO = new CredentialsDAO(); 
        }

        public bool IsValidLogin (Credentials enteredCredentials)
        {
            Credentials validUserCredentils = _CredentialsDAO.FindCredentials(enteredCredentials.Email);

            if (validUserCredentils is null) return false;

            UserRole = validUserCredentils.Role;
            CredentialId = validUserCredentils.Id;

            return (validUserCredentils.Password == enteredCredentials.Password);
        }

        public Role GetUserRole()
        {
            return UserRole;
        }

        public int GetUserCredentialId()
        {
            return CredentialId;
        }
    }
}
