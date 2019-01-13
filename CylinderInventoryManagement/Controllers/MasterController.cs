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
    public class MasterController : Controller
    {
        private IMasters _masters;
        public MasterController()
        {
            this._masters = new MasterRepository();
        }
        // GET: Master
        public ActionResult CreateSubCategory()
        {            
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubCategory(ClsSubCategoryMasterModel clsSubCategoryMaster)
        {
            if (ModelState.IsValid) {
                ClsResponseModel response = await this._masters.Create_SubCategoryAsync(clsSubCategoryMaster);
                if (response.IsSuccess)
                {
                    TempData["ErrorMsg"] = "Success:";
                    return View();
                }
                else
                {
                    TempData["ErrorMsg"] = "Error:";
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
    }
}
