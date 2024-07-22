using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BAL.IServices
{
    public interface ITransactionService
    {
        public List<Transaction> GetTransactions();
    }
}
