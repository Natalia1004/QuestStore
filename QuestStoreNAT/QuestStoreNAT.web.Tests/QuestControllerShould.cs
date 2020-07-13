using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using QuestStoreNAT.web.Controllers;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using Xunit;

namespace QuestStoreNAT.web.Tests
{
    public class QuestControllerShould
    {
        private readonly QuestController _sutController;
        private readonly Mock<ILogger<QuestController>> _mockLogger;
        private readonly Mock<IDB_GenericInterface<Quest>> _mockQuestDao;
        private readonly Mock<HttpResponse> _mockHttpResponse;

        public QuestControllerShould()
        {
            _mockLogger = new Mock<ILogger<QuestController>>();
            _mockQuestDao = new Mock<IDB_GenericInterface<Quest>>();

            _mockHttpResponse = new Mock<HttpResponse>();
            _mockHttpResponse.SetupAllProperties();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupAllProperties();
            mockHttpContext.SetupGet(x => x.Response).Returns(_mockHttpResponse.Object);

            var mockTempData = new Mock<ITempDataDictionary>();

            _sutController = new QuestController( _mockLogger.Object, _mockQuestDao.Object)
            {
                TempData = mockTempData.Object,
                ControllerContext = new ControllerContext {HttpContext = mockHttpContext.Object}
            };
        }

        [Fact]
        public void ReturnsDeleteView_When_PassedValidIdTo_DeleteAction()
        {
            _mockQuestDao.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns(new Quest() { Id = 1 });
             
            var actionResult = _sutController.DeleteQuest(1) as ViewResult;  

            Assert.NotNull(actionResult);
            Assert.Equal("DeleteQuest", actionResult.ViewName);
        }

        [Fact]
        public void ReturnDeleteViewWithModel_When_PassedValidIdTo_DeleteAction()
        {
            _mockQuestDao.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns(new Quest() { Id = 1 });

            var actionResult = _sutController.DeleteQuest(1) as ViewResult;
            var quest = actionResult.ViewData.Model;

            Assert.NotNull(actionResult);
            Assert.IsType<Quest>(quest);
        }

        [Fact]
        public void ReturnNotFoundView_When_ModelIsNullIn_DeleteAction()
        {
            _mockQuestDao.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns((Quest)null);

            var actionResult = _sutController.DeleteQuest(1) as ViewResult;

            Assert.NotNull(actionResult);
            Assert.Equal("NotFound", actionResult.ViewName);
            _mockHttpResponse.VerifySet(x => x.StatusCode = 404);
        }

        [Fact]
        public void SetStatusCodeTo404_When_ModelIsNullIn_DeleteAction()
        {
            _mockQuestDao.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns((Quest)null);

            var actionResult = _sutController.DeleteQuest(1) as ViewResult;

            _mockHttpResponse.VerifySet(x => x.StatusCode = 404);
        }

        [Fact]
        public void RedirectToActionViewAllQuests_WhenValidQuestIsPassedTo_DeleteAction()
        {
            _mockQuestDao.Setup(x => x.FindOneRecordBy(It.IsAny<int>())).Returns(new Quest() { Id = 1 });

            var actionResult = (RedirectToActionResult)_sutController.DeleteQuest(new Quest() { Id = 1 });

            Assert.NotNull(actionResult);
            Assert.Equal("ViewAllQuests", actionResult.ActionName);
        }

        [Fact]
        public void ReturnNotFoundView_When_PassedNegativeIdTo_DeleteAction()
        {
            var actionResult = _sutController.DeleteQuest(-1) as ViewResult;

            Assert.Equal("NotFound", actionResult.ViewName);
        }

        [Fact]
        public void ReturnErrorView_When_PassedNullQuestTo_DeleteAction()
        {
            var actionResult = _sutController.DeleteQuest(null) as ViewResult;

            Assert.Equal("Error", actionResult.ViewName);
        }

        [Fact]
        public void ReturnViewAllQuestsView_When_SuccesfulDbRetrivalIn_ViewAllQuests()
        {
            _mockQuestDao.Setup(x => x.FetchAllRecords()).Returns(new List<Quest>());

            var actionResult = _sutController.ViewAllQuests() as ViewResult;

            Assert.Equal("ViewAllQuests", actionResult.ViewName);
        }

        [Fact]
        public void ReturnErrorView_When_DbReturnsNoQuestsIn_ViewAllQuests()
        {
            _mockQuestDao.Setup(x => x.FetchAllRecords()).Returns((List<Quest>)null);

            var actionResult = _sutController.ViewAllQuests() as ViewResult;

            Assert.Equal("Error", actionResult.ViewName);
        }

        [Fact]
        public void LogError_When_DbReturnsNoQuestsIn_ViewAllQuests()
        {
            _mockQuestDao.Setup(x => x.FetchAllRecords()).Returns((List<Quest>)null);

            var actionResult = _sutController.ViewAllQuests();

            _mockLogger.Verify(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), 
                Times.Once);
        }

        [Fact]
        public void ReturnAddView_When_Calls_AddQuests()
        {
            var actionResult = _sutController.AddQuest() as ViewResult;

            Assert.Equal("AddQuest", actionResult.ViewName);
        }
    }
}
