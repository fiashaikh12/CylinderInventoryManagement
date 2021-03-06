﻿using BusinessEntities;
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
using CylinderInventoryManagement.Common;
namespace CylinderInventoryManagement.Controllers
{
    [SessionTimeout]
    public class CustomerController : Controller
    {

        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly IMasters _masters;
        private ClsResponseModel<List<ClsCategoryMasterModel>> clsResponse;

        public CustomerController()
        {
            this._user = new UserRepository();
            this._product = new ProductRepository();
            this._masters = new MasterRepository();
            this.clsResponse = (ClsResponseModel<List<ClsCategoryMasterModel>>)this._masters.Get_Category();
            ViewBag.Category = clsResponse.Data.Select(x =>
             new SelectListItem
             {
                 Text = x.CategoryName,
                 Value = Convert.ToString(x.CategoryId)
             }
           ).ToList();

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

        [HttpGet]
        public ActionResult CreateRateCard()
        {
            ClsRateCard ratecard = new ClsRateCard();
            return View(ratecard);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRateCard(ClsRateCard rateCard)
        {
            if (rateCard != null)
            {
                rateCard.UserId = (int)Session["userId"];
                var response = await this._user.AddRateCardAsync(rateCard);
                if (response.IsSuccess)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                    //ViewBag.Message = "Rate card created successfully";
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                    //ViewBag.Message = "Failed to create rate card for this user";
                }
            }
            else
            {
                return Json(400, JsonRequestBehavior.AllowGet);
                //ViewBag.Message = "Something went wrong";
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetRateCardDetails(int userId)
        {
            ClsResponseModel<IEnumerable<ClsRateCard>> lstRateCards = await this._user.GetRateCardDetailsByUser(userId) as ClsResponseModel<IEnumerable<ClsRateCard>>;
            return PartialView("/Views/_RateCardDetailsPartial.cshtml", lstRateCards.Data);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateOnlyIncomingValues]
        public async Task<ActionResult> CreateCustomer(ClsCustomerModel clsCustomerModel)
        {
            clsCustomerModel.Password = "NA";
            if (ModelState.IsValid)
            {
                clsCustomerModel.UserId = Convert.ToString(Session["userId"]);
                clsCustomerModel.BusinessId = Convert.ToString(Session["businessId"]);
                clsCustomerModel.TypeId = 1;
                ClsResponseModel clsResponse = await this._user.CreateCustomerAsync(clsCustomerModel);
                if (clsResponse.IsSuccess)
                {
                    ViewBag.UserMessage = 200;
                    ViewBag.Message = clsResponse.Message;
                    return View("Index");
                }
                else
                {
                    ViewBag.UserMessage = 0;
                    return View("Index");
                }
            }
            else
            {
                ViewBag.UserMessage = 400;
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
                                 where customer.CompanyName.ToLower().Contains(searchText.ToLower()) && customer.BusinessId.Contains(Convert.ToString(Session["businessId"]))
                                 select new
                                 {
                                     id = customer.UserId,
                                     label = customer.CompanyName,
                                     name = customer.CompanyName,
                                     address = customer.Address,
                                     mobile = customer.Mobile,
                                     depositamount = customer.DepositAmount,
                                     notes = customer.Notes,
                                     alternatenumber = customer.AlternateNumber
                                 }).ToList();
                return Json(customers, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult CustomerNote(int UserId, string Notes)
        {
            if (Notes != null && UserId != 0)
            {
                var response = this._user.UpdateCustomerNote(UserId, Notes);
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCustomerLadger(int Userid, int ProductId, int FullIssue, int Return, int Trail_id)
        {
            ClsResponseModel responseModel = await this._user.UpdateCustomerLadger(Userid, ProductId, FullIssue, Return, Trail_id, Convert.ToInt32(Session["userId"]));
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
        public async Task<ActionResult> DeleteCustomerLadger(int Userid, string ChallanNumber)
        {
            ClsResponseModel responseModel = await this._user.DeleteCustomerLadger(Userid, Convert.ToInt32(Session["userId"]), ChallanNumber);
            if (responseModel.IsSuccess)
            {
                return Json(new { Status = 1 });
            }
            else
            {
                return Json(new { Status = 0 });
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
            try
            {
                //Helper.WriteErrorLog("functionname : CustomerPurchaseReturnAsync function hit");
                customerPurchaseReturn[0].BusinessId = Convert.ToInt32(Session["businessid"]);
                ClsResponseModel responseModel = await this._product.CustomerPurchaseReturnAsync(customerPurchaseReturn);
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
                Helper.WriteErrorLog("functionname : CustomerPurchaseReturnAsync" + Environment.NewLine + "Source : " + ex.Source.Trim() + Environment.NewLine + ex.Message + Environment.NewLine + "inner exception :" + ex.InnerException);
                return Json(new { Status = 400 });
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
        public ActionResult SearchCustomerReport(int Userid, string fromdate, string todate)
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


        [HttpGet]
        public ActionResult SearchCustomerReportCount(int Userid, string fromdate, string todate)
        {
            ViewBag.CustomerReportCount = this._user.GetCustomerReportCount(Convert.ToInt32(System.Web.HttpContext.Current.Session["businessId"]), Userid, Convert.ToDateTime(fromdate), Convert.ToDateTime(todate));
            return PartialView("/Views/_CustomerReportCount.cshtml", ViewBag.CustomerReportCount.Data);
        }
    }
}