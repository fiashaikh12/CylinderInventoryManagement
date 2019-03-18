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
                clsProduct.UserId = Convert.ToInt32(Session["userId"]);
                clsProduct.BusinessId= Convert.ToInt32(Session["businessId"]);
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

        [HttpPost]
        public async Task<ActionResult> CustomerPurchase(ClsCustomerPurchase clsCustomerPurchase)
        {
            clsCustomerPurchase.BusinessId = Convert.ToInt32(Session["businessId"]);
            clsCustomerPurchase.IsDepositGiven = true;
            clsCustomerPurchase.IsDepositReturn = false;

            clsCustomerPurchase.SubCategoryId = clsResponseModel.Data.Where(x=>x.SubCategoryName==clsCustomerPurchase.SubCategoryId).Select(y=>y.SubCategoryId).FirstOrDefault().ToString();
            clsCustomerPurchase.CategoryId = clsResponse.Data.Where(x => x.CategoryName == clsCustomerPurchase.CategoryId).Select(y => y.CategoryId).FirstOrDefault().ToString();

            ClsResponseModel purchaseResponse = await this._product.CustomerPurchaseAsync(clsCustomerPurchase);
            if (purchaseResponse.IsSuccess)
            {
                return Json(new { Status=1 });
            }
            else
            {
                return Json(new { Status = 0 });
            }
        }

        [HttpGet]
        public  ActionResult ViewProduct(int businessId)
        {
                ClsResponseModel<List<ClsProductDetailModel>> clsResponseModel =(ClsResponseModel<List<ClsProductDetailModel>>)this._product.GetAllProduct(businessId);
                if (clsResponseModel.IsSuccess)
                {
                return  View(clsResponseModel.Data);
                    //return Json(new { data = clsResponseModel.Data });
                }
                else
                {
                return View(clsResponseModel.Data);
                //return Json("");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CustomerReturn(ClsCustomerPurchase clsCustomerReturn)
        {
            clsCustomerReturn.BusinessId = Convert.ToInt32(Session["businessId"]);
            ClsResponseModel purchaseResponse = await this._product.CustomerReturnAsync(clsCustomerReturn);
            if (purchaseResponse.IsSuccess)
            {
                return Json(new { Status = 1 });
            }
            else
            {
                return Json(new { Status = 0 });
            }
        }
    }
}