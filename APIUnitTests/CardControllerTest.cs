
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Bank.Controllers;
using BAL.IServices;
using Common.Requests;
using System.Collections.Generic;
using Bank.Models;


namespace APIUnitTests
{
    [TestClass]
    public class CardControllerTests
    {
        private readonly Mock<ICardService> _mockCardService;
        private readonly CardController _controller;

        public CardControllerTests()
        {
            _mockCardService = new Mock<ICardService>();
            _controller = new CardController(_mockCardService.Object);
        }

        [TestMethod]
        public void GetCardById_CardExists_ReturnsOkResult()
        {
            // Arrange
            var request = new GetCardByIdRequest { Id = 1 };
            var expectedCard = new Card { Id = 1, AccountId = 123, CardNumber = "1234567812345678", IsActive = true };
            _mockCardService.Setup(service => service.GetCardById(request)).Returns(expectedCard);

            // Act
            var result = _controller.GetAccountById(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCard = okResult.Value as Card;
            Assert.IsNotNull(returnedCard);
            Assert.AreEqual(expectedCard.Id, returnedCard.Id);
        }

        [TestMethod]
        public void GetCardById_CardDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var request = new GetCardByIdRequest { Id = 1 };
            _mockCardService.Setup(service => service.GetCardById(request)).Returns(null as Card);

            // Act
            var result = _controller.GetAccountById(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void GetCardsByAccountId_CardsExist_ReturnsOkResult()
        {
            // Arrange
            var request = new GetAccountByIdRequest { Id = 123 };
            var expectedCards = new List<Card>
            {
                new Card { Id = 1, AccountId = 123, CardNumber = "1234567812345678", IsActive = true },
                new Card { Id = 2, AccountId = 123, CardNumber = "8765432187654321", IsActive = true }
            };
            _mockCardService.Setup(service => service.GetCardByAccountId(request)).Returns(expectedCards);

            // Act
            var result = _controller.GetAccountByUserId(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCards = okResult.Value as List<Card>;
            Assert.IsNotNull(returnedCards);
            Assert.AreEqual(2, returnedCards.Count);
        }

        [TestMethod]
        public void GetCardsByAccountId_NoCardsExist_ReturnsNotFoundResult()
        {
            // Arrange
            var request = new GetAccountByIdRequest { Id = 123 };
            _mockCardService.Setup(service => service.GetCardByAccountId(request)).Returns(new List<Card>());

            // Act
            var result = _controller.GetAccountByUserId(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void AddCard_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new AddCardRequest { AccountId = 123, EmbossedName = "John Doe" };
            _mockCardService.Setup(service => service.AddCard(request)).Returns(1);

            // Act
            var result = _controller.AddCard(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Card successfully created", okResult.Value);
        }

        [TestMethod]
        public void AddCard_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new AddCardRequest { AccountId = 123, EmbossedName = "John Doe" };
            _mockCardService.Setup(service => service.AddCard(request)).Returns(0);

            // Act
            var result = _controller.AddCard(request);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("could not create the card 0", badRequestResult.Value);
        }

        [TestMethod]
        public void UpdateCard_CardExists_ReturnsOkResult()
        {
            // Arrange
            var request = new UpdateCardRequest { CardId = 1, AccountId = 123, CardNumber = "1234567812345678", EmbossedName = "John Doe" };
            var card = new Card { Id = 1, AccountId = 123, CardNumber = "1234567812345678", IsActive = true };
            _mockCardService.Setup(service => service.GetCardById(It.IsAny<GetCardByIdRequest>())).Returns(card);
            _mockCardService.Setup(service => service.UpdateCard(request)).Returns(1);

            // Act
            var result = _controller.UpdateCard(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedCard = okResult.Value as Card;
            Assert.IsNotNull(updatedCard);
            Assert.AreEqual(card.Id, updatedCard.Id);
        }

        [TestMethod]
        public void UpdateCard_CardDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var request = new UpdateCardRequest { CardId = 1, AccountId = 123, CardNumber = "1234567812345678", EmbossedName = "John Doe" };
            _mockCardService.Setup(service => service.GetCardById(It.IsAny<GetCardByIdRequest>())).Returns(null as Card);

            // Act
            var result = _controller.UpdateCard(request);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("No card found with this id .", ((Exception)badRequestResult.Value).Message);
        }

        [TestMethod]
        public void ActivateCard_CardExists_ReturnsOkResult()
        {
            // Arrange
            var cardId = 1;
            var card = new Card { Id = cardId, AccountId = 123, CardNumber = "1234567812345678", IsActive = false };
            _mockCardService.Setup(service => service.GetCardById(It.IsAny<GetCardByIdRequest>())).Returns(card);

            // Act
            var result = _controller.ActivateCard(cardId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Card successefully Activated", okResult.Value);
        }

        [TestMethod]
        public void ActivateCard_CardDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var cardId = 1;
            _mockCardService.Setup(service => service.GetCardById(It.IsAny<GetCardByIdRequest>())).Returns(null as Card);

            // Act
            var result = _controller.ActivateCard(cardId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Card not found", notFoundResult.Value);
        }

        [TestMethod]
        public void DeactivateCard_CardExists_ReturnsOkResult()
        {
            // Arrange
            var cardId = 1;
            var card = new Card { Id = cardId, AccountId = 123, CardNumber = "1234567812345678", IsActive = true };
            _mockCardService.Setup(service => service.GetCardById(It.IsAny<GetCardByIdRequest>())).Returns(card);

            // Act
            var result = _controller.DeactivateCard(cardId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Card successefully Deactivated", okResult.Value);
        }

        [TestMethod]
        public void DeactivateCard_CardDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var cardId = 1;
            _mockCardService.Setup(service => service.GetCardById(It.IsAny<GetCardByIdRequest>())).Returns(null as Card);

            // Act
            var result = _controller.DeactivateCard(cardId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Card not found .Could not delete", notFoundResult.Value);
        }



    }
}
