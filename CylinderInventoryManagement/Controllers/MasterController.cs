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
        // GET: Master
        public ActionResult SubCategory()
        {
            return View();
        }
        [HttpPost,ActionName("SubCategory")]
        public async Task<ActionResult> CreateSubCategory(ClsSubCategoryMasterModel clsSubCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                ClsResponseModel response = await this._masters.Create_SubCategoryAsync(clsSubCategoryMaster);
                if (response.IsSuccess)
                {
                    TempData["ErrorMsg"] = response.Message;
                    return View();
                }
                else
                {
                    TempData["ErrorMsg"] = response.Message;
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Master/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Master/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Master/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Master/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Master/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Master/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
