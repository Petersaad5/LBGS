using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAL.Services;
using DAL;
using Bank.Models;
using Common.Requests;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.IO;

namespace ServiceUnitTests
{
    [TestClass]
    public class CardServiceTest
    {
        private CardService? _cardService;
        private DapperAccess? _dapperAccess;

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
            var result = _cardService.GetCardById(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCard.Id, result.Id);
            Assert.AreEqual(expectedCard.AccountId, result.AccountId);
            Assert.AreEqual(expectedCard.CardNumber, result.CardNumber);
            Assert.AreEqual(expectedCard.EmbossedName, result.EmbossedName);
            Assert.AreEqual(expectedCard.ExpiryDate, result.ExpiryDate);
            Assert.AreEqual(expectedCard.IsActive, result.IsActive);
            Assert.AreEqual(expectedCard.CSV, result.CSV);
        }

        [TestMethod]
        public void GetCardById_CardDoesNotExist_ReturnsNull()
        {
            // Arrange
            var request = new GetCardByIdRequest { Id = 9999 }; 

            // Act
            var result = _cardService.GetCardById(request);

            // Assert
            Assert.IsNull(result);
        }
    }
}
