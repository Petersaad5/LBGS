using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public class WithdrawOrDepositRequest
    {
        public string cardNumber {  get; set; } = string.Empty;
        public decimal amount { get; set; }
    }
}
