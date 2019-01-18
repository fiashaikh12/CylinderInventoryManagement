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
        private readonly ClsResponseModel<List<ClsSubCategoryMasterModel>> clsResponseModel;
        private readonly ClsResponseModel<List<ClsCategoryMasterModel>> clsResponse;
        private readonly IProduct _product;

        public ProductController()
        {
            this._masters = new MasterRepository();
            this.clsResponseModel = (ClsResponseModel<List<ClsSubCategoryMasterModel>>)this._masters.Get_SubCategory();
            this.clsResponse = (ClsResponseModel<List<ClsCategoryMasterModel>>)this._masters.Get_Category();
            this._product = new ProductRepository();
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
                ClsResponseModel clsResponseModel =await this._product.AddProductAsync(clsProduct);
                if (clsResponseModel.IsSuccess)
                {
                    TempData["Message"] = clsResponseModel.Message;
                }
                else
                {
                    TempData["Message"] = clsResponseModel.Message;
                }
                return View("Index");
            }
            else
            {
                return View("Index");
            }
        }
    }
}