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
        public Account? GetAccountById(GetAccountByIdRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", request.Id);
            return _dapperAccess.Query<Account>("usp_GetAccountById", parameters).FirstOrDefault();
        }
        public Account? GetAccountByNumber (GetAccountByAccountNumberRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__AccountNumber",request.accountNumber);
            return _dapperAccess.Query<Account>("usp_GetAccountByNumber", parameters).FirstOrDefault();
        }

        public List<Account> GetAccountByUserId(GetUserByIdRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__UserId", request.UserId);
            return _dapperAccess.Query<Account>("usp_GetAccountByUserId",parameters).ToList();
        }
        public int UpdateAccount(UpdateAccountRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", request.Id);
            parameters.Add("P__UserId",request.UsertId);
            parameters.Add("P__AccountNumber", request.AccountNumber);
            parameters.Add("P__IsActive",request.IsActive);
            parameters.Add("P__Balance",request.Balance);
            return _dapperAccess.Execute("usp_UpdateAccount", parameters);


        }
        public int AddAccount(AddAccountRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__UserId", request.UserId);
            parameters.Add("P__AccountNumber", request.AccountNumber);
            parameters.Add("P__IsActive", request.IsActive);
            parameters.Add("P__Balance", request.Balance);  
            return _dapperAccess.Execute("usp_AddAccount", parameters);

        }
        public int DeactivateAccount(int acId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", acId);
            return _dapperAccess.Execute("usp_DeactivateAccount", parameters);
        }
        public int ActivateAccount(int acId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", acId);
            return _dapperAccess.Execute("usp_ActivateAccount", parameters);
        }
    }
}
