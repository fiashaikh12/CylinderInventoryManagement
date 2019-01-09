using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.Interface
{
    public interface IUser
    {
        ClsResponseModel AuthenticateUser(ClsUserLoginModel clsUserModel);
        Task<ClsResponseModel> RegisterUser(ClsUserRegistrationModel responseModel);
    }
}
