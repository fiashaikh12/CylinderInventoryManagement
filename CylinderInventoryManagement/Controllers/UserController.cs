using CIM.Filter;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CylinderInventoryManagement.Controllers
{
    [SessionTimeout]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(HttpPostedFileBase httpPostedFile)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}