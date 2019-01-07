using BusinessEntities;
using BusinessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public async Task<ActionResult> Index()
        {
            UserRepository user = new UserRepository();
            ClsUserModel model = new ClsUserModel();
            await user.RegisterUser(model);
            return View();
        }
    }
}