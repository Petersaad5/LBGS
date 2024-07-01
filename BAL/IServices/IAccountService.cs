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
        public Account? GetAccountById(GetOrDeleteAccountByIdRequest request);
        public List<Account> GetAccountByUserId(GetAccountByUserIdRequest request);
        int UpdateAccount(UpdateAccountRequest request);
        //public int Addaccount(AddAccountRequest request);
        // public int Updateaccount(UpdateAccountRequest request);
        //public int Deleteaccount(int id); Idea make this function render the account inactive and not deleted 
    }
}
