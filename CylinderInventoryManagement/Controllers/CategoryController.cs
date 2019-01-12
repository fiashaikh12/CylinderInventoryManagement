using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CIM.BusinessLayer.Repository;
using CIM.Entities;
namespace CylinderInventoryManagement.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            MasterRepository obj = new MasterRepository();
            ClsCategoryMasterModel objmodel= new ClsCategoryMasterModel();
            objmodel.flag = "S";
            var result = obj.GetCategoryAsync(objmodel);
            return View(result);
        }
    }
}