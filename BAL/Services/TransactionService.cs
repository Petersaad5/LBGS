using BAL.IServices;
using Bank.Models;
using Common.Requests;
using DAL;
using DAL.Models;
using Dapper;
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
        public List<Transaction> GetTransactions(GetTransactionsByAccountRequest request)
        {
            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__AccountNumber", request.accountNumber);
            return _dapperAccess.Query<Transaction>("usp_GetTransactions", parameters).ToList();
        }
        public int AddTransaction(AddTransactionRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FromAccount", request.FromAccount);
            parameters.Add("ToAccount", request.ToAccount);
            parameters.Add("CardNumber", request.CardNumber);
            parameters.Add("TransactionType", request.TransactionType);
            parameters.Add("Amount", request.Amount);
            return _dapperAccess.Execute("usp_AddTransaction", parameters);
        }
    }
}
