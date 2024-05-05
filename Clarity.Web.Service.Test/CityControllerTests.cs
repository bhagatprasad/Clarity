using Clarity.Web.Service.Controllers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;

namespace Clarity.Web.Service.Test
{
    [TestFixture]
    public class CityControllerTests
    {
        private CityController _mockCityController;

        private Mock<ICityService> _mockCityService;

        private City _validCity = null;
        public CityControllerTests()
        {
            _mockCityService = new Mock<ICityService>();

            _mockCityController = new CityController(_mockCityService.Object);
        }

        [SetUp]
        public void Setup()
        {
            this._validCity = new City()
            {
                Id = 1,
                Code = "NYC",
                ContryId = 1,
                Name = "New York",
                StateId = 1,
                IsActive = true,
                CreatedBy = 1,
                CreatedOn = DateTimeOffset.Now,
                ModifiedBy = 1,
                ModifiedOn = DateTimeOffset.Now
            };
        }

        [Test]
        public async Task fetchAllCities_Success_ReturnsCitiesObject()
        {
            //Arrenge
            var expectedCities = new List<City>();
            expectedCities.Add(_validCity);

            _mockCityService.Setup(x => x.fetchAllCities()).ReturnsAsync(expectedCities);


            //Act
            var responce = _mockCityController.fetchAllCities();


            //Assert

            Assert.IsNotNull(responce);

            Assert.IsInstanceOf<OkObjectResult>(responce.Result);

            var responceWithData = responce.Result as OkObjectResult;


            Assert.NotNull(responceWithData);


            Assert.AreEqual(200, responceWithData.StatusCode);

            Assert.AreEqual(expectedCities, responceWithData.Value);

        }

        [Test]
        public async Task fetchAllCities_Fails_RetrunsExceptionObject()
        {
            //Arrange
            _mockCityService.Setup(x => x.fetchAllCities()).ThrowsAsync(new Exception("An error occurred while fetching cities"));


            //Act
            var responce = await _mockCityController.fetchAllCities();



            //Assert
            Assert.IsNotNull(responce);

            Assert.IsInstanceOf<ObjectResult>(responce);


            var responceWithData = responce as ObjectResult;

            Assert.IsNotNull(responceWithData);

            Assert.AreEqual(500, responceWithData.StatusCode);
        }

        [Test]
        public async Task InsertOrUpdateCity_Success_ReturnsOkObjectWithMessage()
        {
            //Arrange 
            bool result = true;

            _mockCityService.Setup(x => x.InsertOrUpdateCity(_validCity)).ReturnsAsync(result);

            //Act
            var responce = await _mockCityController.InsertOrUpdateCity(_validCity);


            //Assert
            Assert.IsNotNull(responce);

            Assert.IsInstanceOf<OkObjectResult>(responce);

            var contentWithResult = responce as OkObjectResult;

            Assert.NotNull(contentWithResult);


            Assert.AreEqual(200, contentWithResult.StatusCode);

            Assert.AreEqual("City inserted or updated successfully.", contentWithResult.Value);
        }

        [Test]
        public async Task InsertOrUpdateCity_Fails_ReturnsBadRequestWithMessage()
        {
            //Arange
            bool interfaceResult = false;
            _mockCityService.Setup(x => x.InsertOrUpdateCity(_validCity)).ReturnsAsync(interfaceResult);


            //Act

            var responce = await _mockCityController.InsertOrUpdateCity(_validCity);


            //Assert

            Assert.IsNotNull(responce);

            Assert.IsInstanceOf<BadRequestObjectResult>(responce);

            var contentBadRequest = responce as BadRequestObjectResult;

            Assert.IsNotNull(contentBadRequest);

            Assert.AreEqual(400, StatusCodes.Status400BadRequest);

            Assert.AreEqual("Failed to insert or update city.", contentBadRequest.Value);


        }

        [Test]
        public async Task InsertOrUpdateCity_Fails_ThrowsExceptionWithMessage()
        {
            //Arrange
            _mockCityService.Setup(x => x.InsertOrUpdateCity(_validCity)).ThrowsAsync(new Exception("An error occurred while inserting or updating city"));

            //Act
            var responce = await _mockCityController.InsertOrUpdateCity(_validCity);

            //Assert
            Assert.IsNotNull(responce);

            Assert.IsInstanceOf<ObjectResult>(responce);

            var responceWithData = responce as ObjectResult;

            Assert.IsNotNull(responceWithData);

            Assert.AreEqual(500, responceWithData.StatusCode);

           // Assert.AreSame("An error occurred while inserting or updating city: An error occurred while inserting or updating city", responceWithData.Value);
        }
    }
}