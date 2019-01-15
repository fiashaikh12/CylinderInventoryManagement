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
        IEnumerable<ClsCategoryMasterModel> Get_Category();
        Task<ClsResponseModel> Add_Category(ClsCategoryMasterModel data);
        Task<ClsResponseModel> Delete_Category(int Categoryid);
        Task<ClsResponseModel> Get_SubCategoryAsync();
        Task <ClsResponseModel> Create_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster);
        Task<ClsResponseModel> Delete_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster);
        Task<ClsResponseModel> Update_SubCategoryAsync();
    }
}
