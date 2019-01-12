using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLayer.DbContext;
using BusinessLayer.Repository.Interface;
using CIM.BusinessLayer.Repository.Interface;
using Dapper;
using CIM.Entities;
namespace CIM.BusinessLayer.Repository
{
    public class MasterRepository : DbContextBase, IMasters
    {
        private readonly IDbConnection _dbContext;
        public MasterRepository()
        {
            this._dbContext = GetConnection();
        }

        public IEnumerable<ClsCategoryMasterModel> GetCategoryAsync(ClsCategoryMasterModel ClsCategoryModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@flag", ClsCategoryModel.flag);
            return _dbContext.Query<ClsCategoryMasterModel>("USP_CategoryMaster", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
