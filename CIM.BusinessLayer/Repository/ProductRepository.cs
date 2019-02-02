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
            parameters.Add("@Businessid", clsProductModel.BusinessId);
            int returnValue = await this._dbContext.ExecuteAsync("USP_AddProducts", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Product added successfully.";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Something went wrong";
            }
            return clsResponse;
        }

        public async Task<ClsResponseModel> CustomerPurchaseAsync(ClsCustomerPurchase clsCustomer)
        {
            ClsResponseModel clsResponse = new ClsResponseModel();
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", clsCustomer.UserId);
            parameters.Add("@IsDepositGiven", clsCustomer.IsDepositGiven);
            parameters.Add("@IsDepositReturn", clsCustomer.IsDepositReturn);
            parameters.Add("@DepositAmount", clsCustomer.DepositAmount);
            parameters.Add("@Createdby", clsCustomer.BusinessId);
            parameters.Add("@Categoryid", clsCustomer.CategoryId);
            parameters.Add("@SubCategoryid", clsCustomer.SubCategoryId);
            parameters.Add("@Quantity", clsCustomer.Quantity);
            parameters.Add("@Businessid", clsCustomer.BusinessId);
            int returnValue = await this._dbContext.ExecuteAsync("USP_CustPurchase", parameters, commandType: CommandType.StoredProcedure);
            if (returnValue > 0)
            {
                clsResponse.IsSuccess = true;
                clsResponse.ErrorCode = 200;
                clsResponse.Message = "Purchase successfully.";
            }
            else
            {
                clsResponse.IsSuccess = false;
                clsResponse.ErrorCode = 400;
                clsResponse.Message = "Purchase failed";
            }
            return clsResponse;
        }

        public ClsResponseModel GetAllProduct(int businessId)
        {
            ClsResponseModel<List<ClsProductDetailModel>> clsResponse = new ClsResponseModel<List<ClsProductDetailModel>>();
            var parameters = new DynamicParameters();
            parameters.Add("@Businessid", businessId);
            List<ClsProductDetailModel> clsProducts =this._dbContext.Query<ClsProductDetailModel>("SP_ProductInvetoryList", parameters, commandType: CommandType.StoredProcedure).ToList();
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

        public ClsResponseModel GetPurchasedCylinder(int businessId, int userId)
        {
            ClsResponseModel<List<ClsProductDetailModel>> clsResponse = new ClsResponseModel<List<ClsProductDetailModel>>();
            var parameters = new DynamicParameters();
            parameters.Add("@Businessid", businessId);
            List<ClsProductDetailModel> clsProducts = this._dbContext.Query<ClsProductDetailModel>("SP_CustomerInvetoryList", parameters, commandType: CommandType.StoredProcedure).ToList();
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
    }
}
