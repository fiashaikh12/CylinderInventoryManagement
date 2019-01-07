using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLayer.DbContext;
using BusinessLayer.Repository.Interface;
using Dapper;

namespace BusinessLayer.Repository
{
    public class UserRepository : DbContextBase, IUser
    {
        
        private readonly IDbConnection _dbContext;
        public UserRepository()
        {
            this._dbContext = GetConnection();
        }

        public ClsResponseModel AuthenticateUser(ClsUserModel clsUserModel)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            using (var response = _dbContext.QueryMultiple("Usp_ValidateUser", new { username = clsUserModel.Mobile, password = clsUserModel.Password }, commandType: CommandType.StoredProcedure))
            {

            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> RegisterUser(ClsUserModel responseModel)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@Name", "Firoz");
            parameters.Add("@Password", "123");
            parameters.Add("@Mobile", "9892978625");
            parameters.Add("@CompanyName", "ABC");
            parameters.Add("@TypeId", 1);
            parameters.Add("@BusinessId",1);
            parameters.Add("@GSTNo", "XYZ123");
            parameters.Add("@Address", "Khar");
            parameters.Add("@CreatedOn", DateTime.Now);;
            int returnValue = await this._dbContext.ExecuteAsync("Usp_RegisterUser", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 400;
            }
            return clsResponse;
        }

    }
}
