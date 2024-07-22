using BAL.IServices;
using BAL.Services;
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
        public IActionResult GetTransactions()
        {
            var transactions = _transactionService.GetTransactions();

            if (transactions.Count == 0)
            {
                return NoContent();
            }

            return Ok(transactions);
        }
    }
}
