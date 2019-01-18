using System.Collections.Generic;
using System.Data;
using DbContext;
using CIM.BusinessLayer.Repository.Interface;
using Dapper;
using CIM.Entities;
using System.Threading.Tasks;
using System;
using System.Linq;

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
                parameters.Add("@UserId", clsSubCategoryMaster.UserId=1);
                parameters.Add("@flag", "I");
                int affectedRows =  await this._dbContext.ExecuteAsync("USP_SubCategoryMaster", parameters, commandType: CommandType.StoredProcedure);
                if (affectedRows > 0)
                {
                    clsResponse.IsSuccess = true;
                    clsResponse.ErrorCode = 200;
                    clsResponse.Message = "Success: Successfully created";
                }
                else
                {
                    clsResponse.IsSuccess = false;
                    clsResponse.ErrorCode = 400;
                    clsResponse.Message = "Error: Failed to create sub-category";
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

        public ClsResponseModel Get_Category()
        {
            ClsResponseModel<List<ClsCategoryMasterModel>> clsResponse = new ClsResponseModel<List<ClsCategoryMasterModel>>();
            var parameters = new DynamicParameters();
            parameters.Add("@flag", "S");
            List<ClsCategoryMasterModel> response = _dbContext.Query<ClsCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure).ToList();
            if (response.Count > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success";
                clsResponse.Data = response;
            }
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

        public ClsResponseModel Get_SubCategory()
        {
            ClsResponseModel<List<ClsSubCategoryMasterModel>> clsResponse = new ClsResponseModel<List<ClsSubCategoryMasterModel>> ();
            var parameters = new DynamicParameters();
            parameters.Add("@flag", "S");
            List<ClsSubCategoryMasterModel> clsSubCategories = this._dbContext.Query<ClsSubCategoryMasterModel>("USP_SubCategoryMaster", parameters, commandType: CommandType.StoredProcedure).ToList();
            if (clsSubCategories.Count > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.Message = "Success";
                clsResponse.ErrorCode = 200;
                clsResponse.Data = clsSubCategories;
            }
            return clsResponse;
        }

        public Task<ClsResponseModel> Update_SubCategoryAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
