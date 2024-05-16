using Clarity.Web.Service.Controllers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Web.Service.Test
{
    [TestFixture]
    public class MailBoxControllerTest
    {
        private readonly MailBoxController mailBoxController;
        private readonly Mock<IMailBoxService> mailboxServce;
        private MailBox validMalBox;

        public MailBoxControllerTest()
        {
            this.mailboxServce = new Mock<IMailBoxService>();

            this.mailBoxController = new MailBoxController(mailboxServce.Object);
        }
        [SetUp]
        public void Setup()
        {
            this.validMalBox = new MailBox()
            {
                Title = "Payslip for the month of April Released !.",
                Subject = "Payslip for the month of April Released !.",
                MessageTypeId = 1,
                Message = "Payslip for the month of April Released !.",
                Description = "Payslip for the month of April Released !.",
                FromUser = "admin@betalen.in",
                ToUser = "",
                IsForAll = true,
                HTMLMessage = "<p>Payslip for the month of April Released !.</p>",
                MailBoxId = 1,
                IsActive = true,
                CreatedBy = 1,
                CreatedOn = DateTimeOffset.Now,
                ModifiedBy = 1,
                ModifiedOn = DateTimeOffset.Now
            };
        }

        [Test]
        public async Task GetMailBoxesAsync_Success_ReturnsOkObjectResultWithData()
        {
            //Arrange
            var expectedResult = new List<MailBox>();
            expectedResult.Add(validMalBox);

            mailboxServce.Setup(x => x.GetMailBoxesAsync()).ReturnsAsync(expectedResult);


            //Act
            var response = await mailBoxController.GetMailBoxesAsync();


            //Assert

            Assert.IsNotNull(response);

            Assert.IsInstanceOf<OkObjectResult>(response);

            var contentResponce = response as OkObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, contentResponce.StatusCode);


            Assert.IsInstanceOf<List<MailBox>>(contentResponce.Value);

            var content = contentResponce.Value as List<MailBox>;

            CollectionAssert.AreEqual(expectedResult, content);

            Assert.IsTrue(content.SequenceEqual(expectedResult));

        }

        [Test]
        public async Task GetMailBoxesAsync_throwsexception_ReturnsInternalServerError()
        {
            //arrage
            mailboxServce.Setup(x => x.GetMailBoxesAsync()).ThrowsAsync(new Exception("Internal Server Error"));


            //act

            var response = await mailBoxController.GetMailBoxesAsync();

            Assert.IsNotNull(response);

            Assert.IsInstanceOf<StatusCodeResult>(response);    

            var content= response as StatusCodeResult;

            Assert.AreEqual(StatusCodes.Status500InternalServerError, content.StatusCode);

        }
    }
}
