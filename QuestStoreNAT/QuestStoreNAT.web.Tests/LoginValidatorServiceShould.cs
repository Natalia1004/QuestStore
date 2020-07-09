using Xunit;
using Moq;
using QuestStoreNAT.web.Services;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;

namespace QuestStoreNAT.web.Tests
{
    public class LoginValidatorServiceShould
    {
        private LoginValidatorService sut;
        private Mock<Credentials> mockCredentials;

        public LoginValidatorServiceShould()
        {
            mockCredentials = new Mock<Credentials>();
            mockCredentials.SetupAllProperties();
            mockCredentials.Setup(x => x.Id).Returns(1);

            var mockCredentialsDAO = new Mock<CredentialsDAO>();

            sut = new LoginValidatorService(mockCredentialsDAO.Object);
        }

        [Fact]
        public void ReturnTrue_When_PassedValidCredentials()
        {

        }
    }
}
