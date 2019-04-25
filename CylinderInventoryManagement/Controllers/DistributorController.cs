using BusinessEntities;
using BusinessLayer.Repository;
using BusinessLayer.Repository.Interface;
using CIM.BusinessLayer.Repository;
using CIM.BusinessLayer.Repository.Interface;
using CIM.Entities;
using CIM.Filter;
using CylinderInventoryManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
    [SessionTimeout]
    public class DistributorController : Controller
    {
        private readonly IUser _user;
        private readonly IProduct _product;

        public DistributorController()
        {
            this._user = new UserRepository();
            this._product = new ProductRepository();
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserDepositDetails(int userId)
        {
            ClsResponseModel<IEnumerable<ClsUserDepositDetails>> _depositDetails = null;
            if (userId > 0)
            {
                _depositDetails = await this._product.GetDepositDetails(userId) as ClsResponseModel<IEnumerable<ClsUserDepositDetails>>;
            }
            return PartialView("/Views/_DepositDetailsPartial.cshtml", _depositDetails.Data);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateOnlyIncomingValues]
        public async Task<ActionResult> CreateCustomer(ClsCustomerModel clsCustomerModel)
        {
            if (ModelState.IsValid)
            {
                clsCustomerModel.UserId = Convert.ToString(Session["userId"]);
                clsCustomerModel.BusinessId = Convert.ToString(Session["businessId"]);
                clsCustomerModel.TypeId = 3;
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

        [HttpPost]
        public ActionResult SearchDistributorAuto(string searchText)
        {
            if (searchText != null)
            {
                ClsResponseModel<List<ClsCustomerModel>> customerResponse = (ClsResponseModel<List<ClsCustomerModel>>)this._user.GetDistributorDetails();
                var customers = (from customer in customerResponse.Data
                                 where customer.CompanyName.ToLower().Contains(searchText.ToLower())
                                 select new { id = customer.UserId, label = customer.CompanyName, name = customer.Name, address = customer.Address, mobile = customer.Mobile, depositamount = customer.DepositAmount, alternatenumber = customer.AlternateNumber });
                return Json(customers, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet, ActionName("GetPurchasedCylinder")]
        public ActionResult PurchasedCylinder(int userId)
        {
            ViewBag.PurchasedCylinder = this._product.GetAllEmptyProductandHoldingStock(Convert.ToInt32(Session["businessid"]), userId) as ClsResponseModel<List<ClsProductDetailModel>>;
            return PartialView("/Views/_DistributorCylinder.cshtml", ViewBag.PurchasedCylinder.Data);
        }

        [HttpPost]
        public async Task<ActionResult> DistributorPurchaseReturnAsync(List<ClsCustomerPurchaseReturn> customerPurchaseReturn)
        {
            try
            {
                customerPurchaseReturn[0].BusinessId = Convert.ToInt32(Session["businessid"]);
                ClsResponseModel responseModel = await this._product.DistributorPurchaseReturnAsync(customerPurchaseReturn);
                if (responseModel.IsSuccess)
                {
                    return Json(new { Status = 1 });
                }
                else
                {
                    return Json(new { Status = 0 });
                }
            }
            catch (Exception ex)
            {
                Helper.WriteErrorLog("functionname : DistributorPurchaseReturnAsync" + Environment.NewLine + "Source : " + ex.Source.Trim() + Environment.NewLine + ex.Message + Environment.NewLine + "inner exception :" + ex.InnerException);
                return Json(new { Status = 400 });
            }
        }

        public ActionResult DistributorReport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchCustomerReport(int Userid, string fromdate, string todate)
        {
            ViewBag.CustomerReport = this._user.GetCustomerReport(Convert.ToInt32(System.Web.HttpContext.Current.Session["businessId"]), Userid, Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));
            return PartialView("/Views/_DistributorReport.cshtml", ViewBag.CustomerReport.Data);
        }

    }
}