using CIM.BusinessLayer.Repository;
using CIM.BusinessLayer.Repository.Interface;
using CIM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMasters _masters;
        public ProductController()
        {
            this._masters = new MasterRepository();
            ClsResponseModel<List<ClsSubCategoryMasterModel>> clsResponseModel = (ClsResponseModel<List<ClsSubCategoryMasterModel>>)this._masters.Get_SubCategory();
            ClsResponseModel<List<ClsCategoryMasterModel>> clsResponse = (ClsResponseModel<List<ClsCategoryMasterModel>>)this._masters.Get_Category();

            ViewBag.Category = clsResponse.Data.Select(x => new SelectListItem
            {
                Text = Convert.ToString(x.CategoryName),
                Value = Convert.ToString(x.CategoryId)
            }).ToList();

            ViewBag.SubCategory = clsResponseModel.Data.Select(x => new SelectListItem
            {
                Text = Convert.ToString(x.SubCategoryName),
                Value = Convert.ToString(x.SubCategoryId)
            }).ToList();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost][ActionName("CreateProduct")]
        public async Task<ActionResult> Index(ClsProductModel clsProduct)
        {
            if (ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                return View("Index");
            }
        }
    }
}