using CIM.BusinessLayer.Repository.Interface;
using CIM.Entities;
using Dapper;
using DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.BusinessLayer.Repository
{
    public class ProductRepository : DbContextBase, IProduct
    {
        private readonly IDbConnection _dbContext;
        public ProductRepository()
        {
            this._dbContext = GetConnection();
        }
        public async Task<ClsResponseModel> AddProductAsync(ClsProductModel clsProductModel)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@Categoryid", clsProductModel.CategoryId);
            parameters.Add("@SubCategoryid", clsProductModel.SubCategoryId);
            parameters.Add("@Quantity", clsProductModel.Quantity);
            parameters.Add("@IsDepositAllowed", clsProductModel.IsDepositAllowed);
            parameters.Add("@IsExchangeAllowed", clsProductModel.IsExchangeAllowed);
            parameters.Add("@UserId", clsProductModel.UserId);
            parameters.Add("@Price", clsProductModel.Price);
            int returnValue = await this._dbContext.ExecuteAsync("USP_AddProducts", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Success: Product added successfully.";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Failed: Something went wrong";
            }
            return clsResponse;
        }
    }
}
