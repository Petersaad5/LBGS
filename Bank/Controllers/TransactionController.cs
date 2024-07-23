using Azure.Core;
using BAL.IServices;
using BAL.Services;
using Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet("GetTransactions")]
        public IActionResult GetTransactions([FromQuery]GetTransactionsByAccountRequest request)
        {
            var transactions = _transactionService.GetTransactions(request);

            if (transactions.Count == 0)
            {
                return NoContent();
            }

            return Ok(transactions);
        }
        //[HttpPost("AddTransaction")] // may not be needed only a service called in atm each time 
        //public IActionResult AddTransaction(AddTransactionRequest request)
        //{
        //    try
        //    {
        //        int affectedRows = _transactionService.AddTransaction(request);

        //        if (affectedRows == 0)
        //        {
        //            return BadRequest($"could not create the card {affectedRows}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok($"Transaction successfully created");
        //}

    }
}
