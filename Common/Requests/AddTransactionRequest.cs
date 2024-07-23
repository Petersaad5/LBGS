using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class AddTransactionRequest
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
