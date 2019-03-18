using CIM.BusinessLayer.Repository;
using CIM.BusinessLayer.Repository.Interface;
using CIM.Entities;
using CIM.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
 
namespace CylinderInventoryManagement.Controllers
{
    [SessionTimeout]
    public class MasterController : Controller
    {
        private IMasters _masters;
        private ClsResponseModel<List<ClsCategoryMasterModel>> clsResponse;
        public MasterController()
        {
            this._masters = new MasterRepository();
           this.clsResponse = (ClsResponseModel<List<ClsCategoryMasterModel>>)this._masters.Get_Category();
            
            ViewBag.Category = clsResponse.Data.Select(x =>
             new SelectListItem
             {
                 Text = x.CategoryName,
                 Value = Convert.ToString(x.CategoryId)
             }
           ).ToList();
        }
        [HttpGet]
        public ActionResult SubCategoryDetails()
        {
            ViewBag.SubCateResponse = this._masters.Get_SubCategory() as ClsResponseModel<List<ClsSubCategoryMasterModel>>;
            return View();
        }
        // GET: Master
        public ActionResult AddSubCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubCategory(ClsSubCategoryMasterModel clsSubCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                ClsResponseModel response = await this._masters.Create_SubCategoryAsync(clsSubCategoryMaster);
                if (response.IsSuccess)
                {
                    ViewData["ErrorMsg"] = response.Message;
                    return RedirectToAction("SubCategory");
                }
                else
                {
                    ViewData["ErrorMsg"] = response.Message;
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteSubCate(int id)
        {
            this._masters.Delete_SubCategory(Convert.ToInt32(Session["businessId"]), id);
            return RedirectToAction("SubCategory");
        }

        //Category Code
        public ActionResult CategoryList()
        {
            if (clsResponse.Data.Count > 0)
            {
                TempData["ErrorMsg"] = "Success:";
                return View(clsResponse.Data);
            }
            else
            {
                TempData["ErrorMsg"] = "Error:";
                return View(clsResponse.Data);
            }

        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(ClsCategoryMasterModel form)
        {
            if (ModelState.IsValid)
            {
                var obj = _masters.Add_Category(form);
                return RedirectToAction("CategoryList");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Deletecategory(int id)
        {
            var obj = _masters.Delete_Category(id);
            return RedirectToAction("CategoryList");
        }

        

    }
}
