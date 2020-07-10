using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using QuestStoreNAT.web.Controllers;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using Xunit;

namespace QuestStoreNAT.web.Tests
{
    public class QuestControllerShould
    {
        private QuestController sutController;
        private Mock<IDB_GenericInterface<Quest>> mockQuestDAO;

        public QuestControllerShould()
        {
            mockQuestDAO = new Mock<IDB_GenericInterface<Quest>>();

            var mockHttpResponse = new Mock<HttpResponse>();
            mockHttpResponse.SetupAllProperties();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupAllProperties();
            mockHttpContext.SetupGet(x => x.Response).Returns(mockHttpResponse.Object);

            var mockTempData = new Mock<ITempDataDictionary>();

            sutController = new QuestController(mockQuestDAO.Object);
            sutController.TempData = mockTempData.Object;
            sutController.ControllerContext = new ControllerContext();
            sutController.ControllerContext.HttpContext = mockHttpContext.Object;
        }

        [Fact]
        public void ReturnDeleteView_When_PassedValidId()
        {
            mockQuestDAO.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns(new Quest() { Id = 1 });
             
            var actionResult = sutController.DeleteQuest(1) as ViewResult;  

            Assert.NotNull(actionResult);
            Assert.Equal("DeleteQuest", actionResult.ViewName);
        }

        [Fact]
        public void ReturnDeleteViewWithModel_When_PassedValidId()
        {
            mockQuestDAO.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns(new Quest() { Id = 1 });

            var actionResult = sutController.DeleteQuest(1) as ViewResult;
            var quest = actionResult.ViewData.Model;

            Assert.NotNull(actionResult);
            Assert.IsType<Quest>(quest);
        }

        [Fact]
        public void ReturnNotFoundView_When_ModelIsNull()
        {
            mockQuestDAO.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns((Quest)null);

            var actionResult = sutController.DeleteQuest(1) as ViewResult;

            Assert.NotNull(actionResult);
            Assert.Equal("NotFound", actionResult.ViewName);
        }

        [Fact]
        public void RedirectToActionViewAllQuests_When_DeletingValidQuest()
        {
            mockQuestDAO.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns(new Quest() { Id = 1 });

            var actionResult = (RedirectToActionResult)sutController.DeleteQuest(new Quest() { Id = 1 });

            Assert.NotNull(actionResult);
            Assert.Equal("ViewAllQuests", actionResult.ActionName);
        }
    }
}
