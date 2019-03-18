using BusinessEntities;
using BusinessLayer.Repository;
using BusinessLayer.Repository.Interface;
using CIM.Entities;
using CIM.Entities.ResponseModel;
using CIM.Filter;
using System.Threading.Tasks;
using System.Web; 
using System.Web.Mvc;
using System.Web.Security;

namespace CylinderInventoryManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController()
        {
            _user = new UserRepository();   
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous,HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(ClsUserLoginModel clsUserLoginModel)
        {
            if (ModelState.IsValid)
            {
                ClsResponseModel<ClsLoginResponse> clsResponse = await this._user.AuthenticateUserAsync(clsUserLoginModel);
                if (clsUserLoginModel.RememberMe)
                {
                    int timeout = clsUserLoginModel.RememberMe ? 525600 : 30;
                    var ticket = new FormsAuthenticationTicket(clsUserLoginModel.Mobile, clsUserLoginModel.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                    {
                        Expires = System.DateTime.Now.AddMinutes(timeout),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);
                }
                if (clsResponse.IsSuccess)
                {
                    Session["userId"] = clsResponse.Data.UserId;
                    Session["businessId"] = clsResponse.Data.BusinessId;
                    Session["businessName"] = clsResponse.Data.BusinessName;
                    return RedirectToAction("Index","Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password provided");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}