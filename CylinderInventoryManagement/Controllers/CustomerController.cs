using BusinessEntities;
using BusinessLayer.Repository;
using BusinessLayer.Repository.Interface;
using CIM.BusinessLayer.Repository;
using CIM.BusinessLayer.Repository.Interface;
using CIM.Entities;
using CIM.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
    [SessionTimeout]
    public class CustomerController : Controller
    {

        private readonly IUser _user;
        private readonly IProduct _product;
        public CustomerController()
        {
            this._user = new UserRepository();
            this._product = new ProductRepository();
            ViewBag.ProductDetail = this._product.GetAllProduct(Convert.ToInt32(System.Web.HttpContext.Current.Session["businessId"]));
            ClsResponseModel<List<ClsCustomerModel>> customerRes = (ClsResponseModel<List<ClsCustomerModel>>)this._user.GetCustomerDetails();
            ViewBag.Customer = customerRes.Data.Select(y => new SelectListItem
            {
                Text = y.Name,
                Value = y.UserId
            }).ToList();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken, ValidateOnlyIncomingValues]
        public async Task<ActionResult> CreateCustomer(ClsCustomerModel clsCustomerModel)
        {
            if (ModelState.IsValid)
            {
                clsCustomerModel.UserId = Convert.ToString(Session["userId"]);
                clsCustomerModel.BusinessId = Convert.ToString(Session["businessId"]);
                clsCustomerModel.TypeId = 1;
                ClsResponseModel clsResponse = await this._user.CreateCustomerAsync(clsCustomerModel);
                if (clsResponse.IsSuccess)
                {
                    ViewBag.Message = clsResponse.Message;
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ViewBag.Message = clsResponse.Message;
                    return View("Index");
                }
            }
            else
            {
                return View();
            }
        }


        public ActionResult CheckExistingMobileNumber(string mobileNumber)
        {
            bool IsMobileExist = false;
            try
            {
                IsMobileExist = mobileNumber.Equals("mukeshknayak@gmail.com") ? true : false;
                return Json(!IsMobileExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult SearchCustomerAuto(string searchText)
        {
            if (searchText != null)
            {
                ClsResponseModel<List<ClsCustomerModel>> customerResponse = (ClsResponseModel<List<ClsCustomerModel>>)this._user.GetCustomerDetails();
                var customers = (from customer in customerResponse.Data
                                 where customer.Name.Contains(searchText)
                                 //& customer.BusinessId.Equals(Convert.ToInt32(System.Web.HttpContext.Current.Session["businessId"]))
                                 //& customer.IsActive.Equals(true)
                                 //select new { customer.UserId});
                                 select new { id = customer.UserId, label = customer.Name, name = customer.Name,address= customer.Address ,mobile=customer.Mobile,depositamount=customer.DepositAmount});
                return Json(customers, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost, ActionName("GetPurchasedCylinder")]
        //public ActionResult PurchasedCylinder(int userId)
        //{
        //    ViewBag.PurchasedCylinder = this._product.GetPurchasedCylinder(Convert.ToInt32(Session["businessid"]), userId) as ClsResponseModel<List<ClsProductDetailModel>>;
        //    return PartialView("/Views/_PurchasedCylinder.cshtml", ViewBag.PurchasedCylinder.Data);
        //}

        [HttpPost]
        public async Task<ActionResult> CustomerPurchaseReturnAsync(List<ClsCustomerPurchaseReturn> customerPurchaseReturn)
        {
            customerPurchaseReturn[0].BusinessId = Convert.ToInt32(Session["businessid"]);
            ClsResponseModel responseModel= await this._product.CustomerPurchaseReturnAsync(customerPurchaseReturn);
            if (responseModel.IsSuccess)
            {
                return Json(new { Status = 1 });
            }
            else
            {
                return Json(new { Status = 0 });
            }
        }
        [HttpPost]
        public async Task<ActionResult> CustomerDepositAsync(ClsCustomerDeposiit customerDeposiit)
        {
            customerDeposiit.BusinessId = Convert.ToInt32(Session["businessid"]);
            ClsResponseModel responseModel = await this._product.CustomerDepositAsync(customerDeposiit);
            if (responseModel.IsSuccess)
            {
                return Json(new { Status = 1 });
            }
            else
            {
                return Json(new { Status = 0 });
            }
        }

        [HttpGet, ActionName("GetPurchasedCylinder")]
        public ActionResult PurchasedCylinder(int userId)
        {
            ViewBag.PurchasedCylinder = this._product.GetAllProductandHoldingStock(Convert.ToInt32(Session["businessid"]), userId) as ClsResponseModel<List<ClsProductDetailModel>>;
            return PartialView("/Views/_PurchasedCylinder.cshtml", ViewBag.PurchasedCylinder.Data);
        }

        public ActionResult CustomerReport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchCustomerReport(int Userid,string fromdate,string todate)
        {
            //try
            //{
                //ClsResponseModel<List<CustomerReport>> customerResponse = (ClsResponseModel<List<CustomerReport>>)this._user.GetCustomerReport(Convert.ToInt32(System.Web.HttpContext.Current.Session["businessId"]),CustomerReport.UserId, Convert.ToDateTime(CustomerReport.fromdate), Convert.ToDateTime(CustomerReport.todate));
                //ViewBag.CustomerReport = customerResponse;
            //return Json(customerResponse, JsonRequestBehavior.AllowGet);
            //}
            //catch(Exception ex)
            //{
            //    return Json(CustomerReport, JsonRequestBehavior.AllowGet);
            //}
            ViewBag.CustomerReport = this._user.GetCustomerReport(Convert.ToInt32(System.Web.HttpContext.Current.Session["businessId"]), Userid, Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));
            return PartialView("/Views/_CustomerReport.cshtml", ViewBag.CustomerReport.Data);
        }
    }
}