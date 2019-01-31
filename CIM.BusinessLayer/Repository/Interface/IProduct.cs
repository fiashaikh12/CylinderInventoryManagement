using CIM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.BusinessLayer.Repository.Interface
{
    public interface IProduct
    {
        Task<ClsResponseModel> AddProductAsync(ClsProductModel clsProductModel);
        ClsResponseModel GetAllProduct(int businessId);
        Task<ClsResponseModel> CustomerPurchaseAsync(ClsCustomerPurchase clsCustomer);
    }
}
