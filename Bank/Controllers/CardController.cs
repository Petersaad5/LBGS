using BAL.IServices;
using BAL.Services;
using Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet("GetCardById")]
        public IActionResult GetAccountById([FromQuery] GetCardByIdRequest request)
        {
            var card = _cardService.GetCardById(request);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }
        [HttpGet("GetCardsByAccountId")]
        public IActionResult GetAccountByUserId([FromQuery] GetAccountByIdRequest request)
        {
            var cards = _cardService.GetCardByAccountId(request);
            if (cards.Count == 0)
            {
                return NotFound();
            }
            return Ok(cards);

        }
        [HttpPost("AddCard")]
        public IActionResult AddCard(AddCardRequest request)
        {
            try
            {
                int affectedRows = _cardService.AddCard(request);

                if (affectedRows == 0)
                {
                    return BadRequest($"could not create the card {affectedRows}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message );
            }

            return Ok($"Card successfully created");
        }
        [HttpPut("UpdateCard")]
        public IActionResult UpdateCard(UpdateCardRequest request)
        {
            try
            {
                var getCardRequest = new GetCardByIdRequest { Id = request.CardId };
                var card = _cardService.GetCardById(getCardRequest);
                if (card == null) 
                {
                    throw new Exception("No card found with this id .");
                }
                int affectedRows = _cardService.UpdateCard(request);
                if (affectedRows == 0)
                {
                    throw new Exception("No affected rows in the query");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            var getCardRequest2 = new GetCardByIdRequest { Id = request.CardId };
            var updatedCard = _cardService.GetCardById(getCardRequest2);
            return Ok(updatedCard);
        }
        
        [HttpPut("ActivateCard/{id}")]
        public IActionResult ActivateCard(int id)
        {
            var getCardRequest = new GetCardByIdRequest { Id = id };

            if (_cardService.GetCardById(getCardRequest) == null)
            {
                return NotFound("Card not found");
            }
            _cardService.ActivateCard(id);

            return Ok("Card successefully Activated");
        }
        [HttpPut("DeactivateCard/{id}")]
        public IActionResult DeactivateCard(int id)
        {
            var getCardRequest = new GetCardByIdRequest { Id = id };

            if (_cardService.GetCardById(getCardRequest) == null)
            {
                return NotFound("Card not found .Could not delete");
            }
            _cardService.DeactivateCard(id);

            return Ok("Card successefully Deactivated");
        }
    }
}
