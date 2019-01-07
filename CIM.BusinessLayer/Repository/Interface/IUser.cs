using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.Interface
{
    public interface IUser
    {
        ClsResponseModel AuthenticateUser(ClsUserModel clsUserModel);
        Task<ClsResponseModel> RegisterUser(ClsUserModel responseModel);
    }
}
