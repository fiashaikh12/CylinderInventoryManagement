using BusinessEntities;
using CIM.Entities;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.Interface
{
    public interface IUser
    {
        Task<ClsResponseModel> AuthenticateUserAsync(ClsUserLoginModel clsUserModel);
        Task<ClsResponseModel> RegisterUserAsync(ClsUserRegistrationModel responseModel);
    }
}
