using System;
using System.Data;
using System.Threading.Tasks;
using BusinessEntities;
using DbContext;
using BusinessLayer.Repository.Interface;
using Dapper;
using CIM.Entities;
using CIM.Entities.ResponseModel;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Repository
{
    public class UserRepository : DbContextBase, IUser
    {
        
        private readonly IDbConnection _dbContext;
        public UserRepository()
        {
            this._dbContext = GetConnection();
        }

        public async Task<ClsResponseModel<ClsLoginResponse>> AuthenticateUserAsync(ClsUserLoginModel clsUserModel)
        {
            ClsResponseModel<ClsLoginResponse> clsResponse = new ClsResponseModel<ClsLoginResponse>();
            var parameters = new DynamicParameters();
            parameters.Add("@Mobile", clsUserModel.Mobile);
            parameters.Add("@Password", clsUserModel.Password);
            ClsLoginResponse clsStatus = await _dbContext.QuerySingleAsync<ClsLoginResponse>("Usp_UserLogin", parameters, commandType: CommandType.StoredProcedure);
            if (clsStatus.TypeName == "U")
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

        public async Task<ClsResponseModel> CreateCustomerAsync(ClsCustomerModel responseModel)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@Name", responseModel.Name);
            parameters.Add("@Mobile", responseModel.Mobile);
            parameters.Add("@CompanyName", responseModel.CompanyName);
            parameters.Add("@TypeId", responseModel.TypeId);
            parameters.Add("@BusinessId", responseModel.BusinessId);
            parameters.Add("@GSTNo", responseModel.GSTNo);
            parameters.Add("@Address", responseModel.Address);
            parameters.Add("@userId", responseModel.UserId);
            parameters.Add("@AlternateNumber", responseModel.AlternateNumber);
            parameters.Add("@Password", responseModel.Password);
            int returnValue = await this._dbContext.ExecuteAsync("Usp_RegisterCustomer", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Customer created successully";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed to create customer";
            }
            return clsResponse;
        }

        public ClsResponseModel GetCustomerDetails()
        {
            ClsResponseModel<List<ClsCustomerModel>> clsResponse = new ClsResponseModel<List<ClsCustomerModel>>();
            
            List<ClsCustomerModel> response = _dbContext.Query<ClsCustomerModel>("USP_GetCustomer", null, commandType: CommandType.StoredProcedure).ToList();
            if (response.Count > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = response;
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
            }
            return clsResponse;
        }

        public ClsResponseModel GetDistributorDetails()
        {
            ClsResponseModel<List<ClsCustomerModel>> clsResponse = new ClsResponseModel<List<ClsCustomerModel>>();

            List<ClsCustomerModel> response = _dbContext.Query<ClsCustomerModel>("USP_GetDistributor", null, commandType: CommandType.StoredProcedure).ToList();
            if (response.Count > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = response;
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
            }
            return clsResponse;
        }

        public ClsResponseModel GetCustomerReport(int Businessid,int Userid,DateTime fromdate,DateTime todate)
        {
            ClsResponseModel<List<CustomerReportResponse>> clsResponse = new ClsResponseModel<List<CustomerReportResponse>>();
            var parameters = new DynamicParameters();
            parameters.Add("@Businessid", Businessid);
            parameters.Add("@Userid", Userid);
            parameters.Add("@fromdate", fromdate);
            parameters.Add("@todate", todate);
            List<CustomerReportResponse> clsProducts = this._dbContext.Query<CustomerReportResponse>("SP_CustomerReport", parameters, commandType: CommandType.StoredProcedure).ToList();
            if (clsProducts.Count > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = clsProducts;
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
            }
            return clsResponse;
        }

        public ClsResponseModel GetCustomerReportCount(int Businessid, int Userid, DateTime fromdate, DateTime todate)
        {
            ClsResponseModel<List<CustomerReportResponse>> clsResponse = new ClsResponseModel<List<CustomerReportResponse>>();
            var parameters = new DynamicParameters();
            parameters.Add("@Businessid", Businessid);
            parameters.Add("@Userid", Userid);
            parameters.Add("@fromdate", fromdate);
            parameters.Add("@todate", todate);
            List<CustomerReportResponse> clsProducts = this._dbContext.Query<CustomerReportResponse>("SP_CustomerReportCount", parameters, commandType: CommandType.StoredProcedure).ToList();
            if (clsProducts.Count > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = clsProducts;
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> UpdateCustomerLadger(int Userid, int ProductId,int FullIssue_Update,int Return_Update,int Trail_id_Update,int Createdby)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@Userid", Userid);
            parameters.Add("@ProductId", ProductId);
            parameters.Add("@FullIssue_Update", FullIssue_Update);
            parameters.Add("@Return_Update", Return_Update);
            parameters.Add("@Trail_id_Update", Trail_id_Update);
            parameters.Add("@Createdby", Createdby);
            int returnValue = await this._dbContext.ExecuteAsync("Update_TrailDetails", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Leadger Updated successully";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed to update notes";
            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> DeleteCustomerLadger(int Userid, int Updatedby, string ChallanNumber)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", Userid);
            parameters.Add("@Updateby", Updatedby);
            parameters.Add("@ChallanNumber", ChallanNumber);
            int returnValue = await this._dbContext.ExecuteAsync("Delete_challan", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Leadger Deleted successully";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed to delete Leadger";
            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> UpdateCustomerNote(int Userid,string Notes)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@Userid", Userid);
            parameters.Add("@Notes", Notes);
            int returnValue = await this._dbContext.ExecuteAsync("Usp_UpdateCustomerNote", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Notes Updated successully";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed to update notes";
            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> AddRateCardAsync(ClsRateCard rateCard)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@userId", rateCard.CustomerId);
            parameters.Add("@categoryId", rateCard.CategoryId);
            parameters.Add("@rate", rateCard.RateCard);
            parameters.Add("@createdBy", rateCard.UserId);
            int returnValue = await this._dbContext.ExecuteAsync("Usp_AddRateCard", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> GetRateCardDetailsByUser(int userId)
        {
            ClsResponseModel<IEnumerable<ClsRateCard>> clsResponse = new ClsResponseModel<IEnumerable<ClsRateCard>>();
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            IEnumerable<ClsRateCard> lstRateDetails =await this._dbContext.QueryAsync<ClsRateCard>("Usp_GetRateCardDetailsByUser", parameters, commandType: CommandType.StoredProcedure);
            if (lstRateDetails.Count()> 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = lstRateDetails;
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed";
            }
            return clsResponse;
        }
    }
}
