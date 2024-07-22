using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}