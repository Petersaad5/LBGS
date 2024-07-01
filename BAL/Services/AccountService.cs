using BAL.IServices;
using Bank.Models;
using Common.Requests;
using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AccountService : IAccountService
    {
        private readonly DapperAccess _dapperAccess;
        public AccountService(DapperAccess dapperAccess)
        {
            _dapperAccess = dapperAccess;
        }
        public Account? GetAccountById(GetOrDeleteAccountByIdRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", request.Id);
            return _dapperAccess.Query<Account>("usp_GetAccountById", parameters).FirstOrDefault();
        }
    }
}
