using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using CIM.Entities;

namespace CIM.BusinessLayer.Repository.Interface
{
    public interface IMasters
    {
        ClsResponseModel Get_Category();
        Task<ClsResponseModel> Get_SubCategoryAsync();
        Task <ClsResponseModel> Create_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster);
        Task<ClsResponseModel> Delete_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster);
        Task<ClsResponseModel> Update_SubCategoryAsync();
    }
}
