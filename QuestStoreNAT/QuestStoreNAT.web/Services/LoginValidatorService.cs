using LoginForm.Services;
using QuestStoreNAT.web.DatabaseLayer;
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

        public Role GetUserRole()
        {
            return UserRole;
        }

        public bool IsValidLogin(Credentials enteredCredentials)
        {
            Credentials validUserCredentils = _CredentialsDAO.FindCredentials(enteredCredentials.Email);

            if (validUserCredentils is null) return false;

            UserRole = validUserCredentils.Role;
            CredentialId = validUserCredentils.Id;

            return (validUserCredentils.Password == enteredCredentials.Password);
        }

        public bool IsValidPasswordHASH(Credentials enteredCredentials)
        {
            Credentials userCredentialsInDb = _CredentialsDAO.FindCredentials(enteredCredentials.Email);

            if (userCredentialsInDb == null) return false;

            var passwordFromDb = userCredentialsInDb.Password;
            var saltFromDb = userCredentialsInDb.SALT;
            var passwordFromForm = EncryptPassword.CreateHASH(enteredCredentials.Password, saltFromDb);

            if (SlowEquals(passwordFromDb.ConvertStringToByte(), passwordFromForm))
            {
                UserRole = userCredentialsInDb.Role;
                return true;
            }
            return false;
        }

        private bool SlowEquals(byte[] a, byte[] b)
        {
            //Byte comparison to prevent timing attacks
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        public int GetUserCredentialId()
        {
            return CredentialId;
        }
    }
}
