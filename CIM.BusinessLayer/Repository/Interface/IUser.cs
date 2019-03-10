using BusinessEntities;
using CIM.Entities;
using CIM.Entities.ResponseModel;
using System.Threading.Tasks;
using System;
namespace BusinessLayer.Repository.Interface
{
    public interface IUser
    {
        Task<ClsResponseModel<ClsLoginResponse>> AuthenticateUserAsync(ClsUserLoginModel clsUserModel);
        Task<ClsResponseModel> CreateCustomerAsync(ClsCustomerModel responseModel);
        ClsResponseModel GetCustomerDetails();
        ClsResponseModel GetCustomerReport(int Businessid, int Userid, DateTime fromdate, DateTime todate);
        ClsResponseModel GetCustomerReportCount(int Businessid, int Userid, DateTime fromdate, DateTime todate);
        ClsResponseModel GetDistributorDetails();
        Task<ClsResponseModel> UpdateCustomerNote(int Userid, string Notes);
    }
}
