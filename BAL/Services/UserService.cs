using BAL.IServices;
using DAL;
using Bank.Models;
using Bank.Requests;
using Dapper;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly DapperAccess _dapperAccess;
        public UserService(DapperAccess dapperAccess)
        {
            _dapperAccess = dapperAccess;
        }

        public User? GetUser(GetUserByIdRequest request) 
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", request.UserId);

            return _dapperAccess.Query<User>("usp_GetUserById", parameters).FirstOrDefault();    
        }

        public List<User> GetUsers()
        {
            return _dapperAccess.Query<User>("usp_GetUsers", null).ToList();
        }

        public int AddUser()
        {
            throw new NotImplementedException();
        }
    }
}
