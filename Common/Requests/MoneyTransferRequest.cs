using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class MoneyTransferRequest
    {
        public string CardNumber { get; set; } = string.Empty;
        public int AccountNumber { get; set; }
        public decimal ammount { get; set; }
    }
}
