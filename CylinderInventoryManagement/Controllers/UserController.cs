using BusinessEntities;
using BusinessLayer.Repository;
using BusinessLayer.Repository.Interface;
using CIM.Entities;
using CIM.Entities.ResponseModel;
using CIM.Filter;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
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
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(ClsUserLoginModel clsUserLoginModel)
        {
            if (ModelState.IsValid)
            {
                ClsResponseModel<ClsLoginResponse> clsResponse = await this._user.AuthenticateUserAsync(clsUserLoginModel);
                if (clsResponse.IsSuccess)
                {
                    Session["authstatus"] = clsResponse.Data.UserId;
                    Session["businessId"] = clsResponse.Data.BusinessId;
                    return View("Index","Dashboard");
                }
                else
                {
                    return View();
                }
            }
            else
            {
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