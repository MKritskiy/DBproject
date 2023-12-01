using DBproject.BL.Auth;
using DBproject.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBproject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuth authBL;

        public RegisterController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View(new ExecutorModel());
        }
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Index(string Name, string Password)
        {
            if (ModelState.IsValid)
            {
                    await authBL.Register(Name, Password);
                    return Redirect("/");

            }
            return View(new ExecutorModel() { Name=Name, Password=Password});
        }
    }
}
