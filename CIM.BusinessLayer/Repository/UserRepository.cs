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

        public async Task<ClsResponseModel<ClsStatus>> AuthenticateUserAsync(ClsUserLoginModel clsUserModel)
        {
            ClsResponseModel<ClsStatus> clsResponse = new ClsResponseModel<ClsStatus>();
            var parameters = new DynamicParameters();
            parameters.Add("@Mobile", clsUserModel.Mobile);
            parameters.Add("@Password", clsUserModel.Password);
            ClsStatus clsStatus = await _dbContext.QuerySingleAsync<ClsStatus>("Usp_UserLogin", parameters, commandType: CommandType.StoredProcedure);
            if (clsStatus.UserId == 2)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = clsStatus;
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
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
