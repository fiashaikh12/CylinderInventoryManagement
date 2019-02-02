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
        Task<ClsResponseModel> Add_Category(ClsCategoryMasterModel data);
        Task<ClsResponseModel> Delete_Category(int Categoryid);
        ClsResponseModel Get_SubCategory();
        Task <ClsResponseModel> Create_SubCategoryAsync(ClsSubCategoryMasterModel clsSubCategoryMaster);
        ClsResponseModel Delete_SubCategory(int userId, int subCateId);
        Task<ClsResponseModel> Update_SubCategoryAsync();
    }
}
