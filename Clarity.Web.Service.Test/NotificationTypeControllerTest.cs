using Clarity.Web.Service.Controllers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Web.Service.Test
{
    [TestFixture]
    public class NotificationTypeControllerTest
    {
        private NotificationTypeController _mockNotificationTypeController;

        private Mock<INotificationTypeService> _mockNotificationTypeService;

        private NotificationType _ValidNotificationType;

        public NotificationTypeControllerTest()
        {
           _mockNotificationTypeService = new Mock<INotificationTypeService>();
           _mockNotificationTypeController = new NotificationTypeController(_mockNotificationTypeService.Object);
        }

        [SetUp]
        public void Setup()
        {
            this._ValidNotificationType = new NotificationType()
            {
                NotificationTypeId = 1,
                Name = "Test",
                Description = "Test",
                IsActive = true,
                CreatedBy = 13,
                CreatedOn = DateTimeOffset.Now,
                ModifiedBy = 13,
                ModifiedOn = DateTimeOffset.Now,

            };
        }

        [Test]
        public async Task FetchAllNotificationType_Success_ReturnsNotificationObject()
        {
            //Arrange
            var expectedNotificationType = new List<NotificationType>();
            expectedNotificationType.Add(_ValidNotificationType);

            _mockNotificationTypeService.Setup(x => x.FetchAllNotificationType()).ReturnsAsync(expectedNotificationType);
            //Act
            var responce = _mockNotificationTypeController.FetchAllNotificationType();
            //Assert

            Assert.IsNotNull(responce);
            Assert.IsInstanceOf<OkObjectResult>(responce.Result);
            var responcewithData = responce.Result as OkObjectResult;
            Assert.IsNotNull(responcewithData);
            Assert.AreEqual(200, responcewithData.StatusCode);
            Assert.AreEqual(expectedNotificationType, responcewithData.Value);

        }

        [Test]
        public async Task FetchAllNotificationType_Fails_ReturnExceptionObject()
        {
            //Arrange

            _mockNotificationTypeService.Setup(x => x.FetchAllNotificationType()).ThrowsAsync(new Exception("Error Getting NotificationType"));

            //Act

            var responce = await _mockNotificationTypeController.FetchAllNotificationType();

            //Assert
            Assert.IsNotNull(responce);
            Assert.IsInstanceOf<ObjectResult>(responce);
            var responceWithData = responce as ObjectResult;

            Assert.IsNotNull(responceWithData);

            Assert.AreEqual(500, responceWithData.StatusCode);
        }


    }
}
