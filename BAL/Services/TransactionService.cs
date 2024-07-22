using BAL.IServices;
using Bank.Models;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DapperAccess _dapperAccess;
        public TransactionService(DapperAccess dapperAccess)
        {
            _dapperAccess = dapperAccess;
        }
        public List<Transaction> GetTransactions()
        {
            return _dapperAccess.Query<Transaction>("usp_GetTransactions", null).ToList();
        }
    }
}
