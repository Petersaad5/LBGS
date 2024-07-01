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

        public User? GetUser(GetOrDeleteUserByIdRequest request) 
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
        public int UpdateUser(UpdateUserRequest request )// Porblem : changes everything not only what actually changes !!!
        { 
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("P__Id", request.UserId);
            parameters.Add("P__Name", request.UserName);
            parameters.Add("P__Email", request.Email);
            parameters.Add("P__Password", request.Password);
            parameters.Add("P__RiskTypeId", request.RiskTypeId);
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
