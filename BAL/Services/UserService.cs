using BAL.IServices;
using DAL;
using Bank.Models;
using Bank.Requests;
using Dapper;
using Common.Requests;
using Azure.Core;

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

        public int AddUser(AddUserRequest request)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Name",  request.UserName);
            parameters.Add("P__Email", request.Email);
            parameters.Add("P__Password", request.Password);
            parameters.Add("P__RiskTypeId", 4);
            parameters.Add("P__CreatedDate", DateTime.Now);
            parameters.Add("P__LastModifiedDate", DateTime.Now);
            return _dapperAccess.Execute("usp_AddUsers", parameters);

        }
        public int UpdateUser(User user )
        {
            Console.WriteLine(user.UserName);
            Console.WriteLine(user.Email);  
            Console.WriteLine(user.Password);
            Console.WriteLine(user.RiskTypeId);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", user.Id);
            parameters.Add("P__Name", user.UserName);
            parameters.Add("P__Email", user.Email);
            parameters.Add("P__Password", user.Password);
            parameters.Add("P__RiskTypeId", 4);
            parameters.Add("P__LastModifiedDate", DateTime.Now);
            return _dapperAccess.Execute("usp_UpdateUser", parameters);
           
        }
        public int DeleteUser(int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", userId);
            return _dapperAccess.Execute("usp_DeleteUser", parameters);
        }
    }
}
