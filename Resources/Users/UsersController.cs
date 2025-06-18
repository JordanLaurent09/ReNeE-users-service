using Microsoft.AspNetCore.Mvc;

namespace users_service.Resources.Users
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
