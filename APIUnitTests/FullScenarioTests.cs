using BAL.Services;
using Bank.Controllers;
using Bank.Models;
using Common.Requests;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ServiceUnitTests
{
    [TestClass]
    public class FullScenarioTest
    {
        private CardService _cardService;
        private DapperAccess _dapperAccess;
        private CardController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Create a configuration object
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = configBuilder.Build();

            // Initialize DapperAccess with the configuration
            _dapperAccess = new DapperAccess(configuration);
            _cardService = new CardService(_dapperAccess);
            _controller = new CardController(_cardService);
        }

        [TestMethod]
        public void GetCardById_CardExists_ReturnsCard()
        {
            // Arrange
            var request = new GetCardByIdRequest { Id = 6 };
            var expectedCard = new Card
            {
                Id = 6,
                AccountId = 2,
                CardNumber = "9047981912047425",
                EmbossedName = "ELIAS BAROUK",
                ExpiryDate = "2027/07",
                IsActive = true,
                CSV = 548
            };

            // Act
            var result = _controller.GetAccountById(request);
            var okResult = result as OkObjectResult;
            var returnedCard = okResult.Value as Card;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(returnedCard);
            Assert.AreEqual(expectedCard.Id, returnedCard.Id);
            Assert.AreEqual(expectedCard.AccountId, returnedCard.AccountId);
            Assert.AreEqual(expectedCard.CardNumber, returnedCard.CardNumber);
            Assert.AreEqual(expectedCard.ExpiryDate, returnedCard.ExpiryDate);
            Assert.AreEqual(expectedCard.CSV, returnedCard.CSV);
            Assert.AreEqual(expectedCard.EmbossedName, returnedCard.EmbossedName);
            Assert.AreEqual(expectedCard.IsActive, returnedCard.IsActive);

        }

        [TestMethod]
        public void GetCardById_CardDoesNotExist_ReturnsNull()
        {
            // Arrange
            var request = new GetCardByIdRequest { Id = 9999 };

            // Act
            var result = _controller.GetAccountById(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
