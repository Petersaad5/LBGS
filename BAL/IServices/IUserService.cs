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
    public interface IUserService
    {
        public User? GetUser(GetUserByIdRequest request);
        public List<User> GetUsers();
        public int AddUser(AddUserRequest request);
        public int UpdateUser(UpdateUserRequest request);
        public int DeactiveUser(int id);
        public int ActivateUser(int id);
    }
}
