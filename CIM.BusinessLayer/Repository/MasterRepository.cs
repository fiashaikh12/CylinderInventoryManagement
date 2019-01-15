using System.Collections.Generic;
using System.Data;
using DbContext;
using CIM.BusinessLayer.Repository.Interface;
using Dapper;
using CIM.Entities;
using System.Threading.Tasks;
using System;

namespace CIM.BusinessLayer.Repository
{
    public class MasterRepository : DbContextBase, IMasters
    {
        private readonly IDbConnection _dbContext;
        public MasterRepository()
        {
            this._dbContext = GetConnection();
        }

        public async Task<ClsResponseModel> Create_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Categoryid", clsSubCategoryMaster.CategoryId);
                parameters.Add("@SubCategoryName", clsSubCategoryMaster.SubCategoryName);
                parameters.Add("@UserId", clsSubCategoryMaster.UserId);
                parameters.Add("@flag", "I");
                int returnValue = await  this._dbContext.ExecuteAsync("USP_SubCategoryMaster", parameters, commandType: CommandType.StoredProcedure);
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
            }
            catch(Exception ex)
            {
                throw;
            }
            return clsResponse;
        }

        public Task<ClsResponseModel> Delete_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ClsCategoryMasterModel> Get_Category()
        {
            // ClsResponseModel<IEnumerable<ClsCategoryMasterModel>> clsResponse = new ClsResponseModel<IEnumerable<ClsCategoryMasterModel>>();
            IEnumerable<ClsCategoryMasterModel> clsResponse = new List<ClsCategoryMasterModel>();
            var parameters = new DynamicParameters();
            parameters.Add("@flag", "S");
            //IEnumerable<ClsCategoryMasterModel> response = _dbContext.Query<ClsCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure);
            //clsResponse.IsSuccess = true;
            //clsResponse.ErrorCode = 200;
            //clsResponse.Message = "Success";
            //clsResponse.Data = response;
            clsResponse = _dbContext.Query<ClsCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure);
            return clsResponse;
        }

        public async Task<ClsResponseModel> Add_Category(ClsCategoryMasterModel data)
        {
            ClsResponseModel<IEnumerable<ClsCategoryMasterModel>> clsResponse = new ClsResponseModel<IEnumerable<ClsCategoryMasterModel>>();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", "I");
            parameters.Add("@CategoryName", data.CategoryName);
            parameters.Add("@UserId", 1);
            
            
            IEnumerable<ClsCategoryMasterModel> models = await _dbContext.QueryAsync<ClsCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure);
            clsResponse.IsSuccess = true;
            clsResponse.Message = "Message";
            clsResponse.ErrorCode = 200;
            clsResponse.Data = models;
            return clsResponse;
        }

        public async Task<ClsResponseModel> Delete_Category(int Categoryid)
        {
            ClsResponseModel<IEnumerable<ClsCategoryMasterModel>> clsResponse = new ClsResponseModel<IEnumerable<ClsCategoryMasterModel>>();

            var parameters = new DynamicParameters();
            parameters.Add("@flag", "D");
            parameters.Add("@Categoryid", Categoryid);
            parameters.Add("@UserId", 1);
            parameters.Add("@IsActive", 0);

            IEnumerable<ClsCategoryMasterModel> models = await _dbContext.QueryAsync<ClsCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure);
            clsResponse.IsSuccess = true;
            clsResponse.Message = "Message";
            clsResponse.ErrorCode = 200;
            clsResponse.Data = models;
            return clsResponse;
        }

        public async Task<ClsResponseModel> Get_SubCategoryAsync()
        {
            ClsResponseModel<IEnumerable<ClsSubCategoryMasterModel>> clsResponse = new ClsResponseModel<IEnumerable < ClsSubCategoryMasterModel >> ();
            var parameters = new DynamicParameters();
            parameters.Add("@flag", "S");

            IEnumerable<ClsSubCategoryMasterModel> models = await _dbContext.QueryAsync<ClsSubCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure);
            clsResponse.IsSuccess = true;
            clsResponse.Message = "Message";
            clsResponse.ErrorCode = 200;
            clsResponse.Data = models;
            return clsResponse;
        }

        public Task<ClsResponseModel> Update_SubCategoryAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
