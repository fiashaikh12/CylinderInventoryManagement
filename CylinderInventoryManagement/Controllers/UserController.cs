using BusinessEntities;
using BusinessLayer.Repository;
using BusinessLayer.Repository.Interface;
using CIM.Filter;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
    [SessionTimeout]
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController()
        {
            _user = new UserRepository();   
        }
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(ClsUserLoginModel clsUserLoginModel)
        {
            if (ModelState.IsValid)
            {
                ClsResponseModel clsResponse = await this._user.AuthenticateUser(clsUserLoginModel);
                return View();
            }
            else
            {
                ModelState.AddModelError("", "");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}