using BusinessEntities;
using CIM.Entities;
using CIM.Entities.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.Interface
{
    public interface IUser
    {
        Task<ClsResponseModel<ClsLoginResponse>> AuthenticateUserAsync(ClsUserLoginModel clsUserModel);
        Task<ClsResponseModel> RegisterUserAsync(ClsUserRegistrationModel responseModel);
    }
}
