using Bank.Models;
using Bank.Requests;
using Common.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IAccountService
    {
        public Account? GetAccountById(GetAccountByIdRequest request);
        public List<Account> GetAccountByUserId(GetUserByIdRequest request);
        public int UpdateAccount(UpdateAccountRequest request);
        public int AddAccount(AddAccountRequest request);
        public int DeleteAccount(int acId);
    }
}
