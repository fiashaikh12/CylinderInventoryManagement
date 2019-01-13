using System;
using System.Data;
using System.Threading.Tasks;
using BusinessEntities;
using DbContext;
using BusinessLayer.Repository.Interface;
using Dapper;
using CIM.Entities;

namespace BusinessLayer.Repository
{
    public class UserRepository : DbContextBase, IUser
    {
        
        private readonly IDbConnection _dbContext;
        public UserRepository()
        {
            this._dbContext = GetConnection();
        }

        public async Task<ClsResponseModel> AuthenticateUserAsync(ClsUserLoginModel clsUserModel)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            using (var response = await _dbContext.QueryMultipleAsync("Usp_ValidateUser", new { username = clsUserModel.MobileNumber, password = clsUserModel.Password }, commandType: CommandType.StoredProcedure))
            {

            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> RegisterUserAsync(ClsUserRegistrationModel responseModel)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@Name", responseModel.Name);
            parameters.Add("@Password", responseModel.Password);
            parameters.Add("@Mobile", responseModel.Mobile);
            parameters.Add("@CompanyName", responseModel.CompanyName);
            parameters.Add("@TypeId", responseModel.TypeId);
            parameters.Add("@BusinessId", responseModel.BusinessId);
            parameters.Add("@GSTNo", responseModel.GSTNo);
            parameters.Add("@Address", responseModel.Address);
            parameters.Add("@CreatedOn", DateTime.Now);;
            int returnValue = await this._dbContext.ExecuteAsync("Usp_RegisterUser", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Success";
            }
            return clsResponse;
        }

    }
}
