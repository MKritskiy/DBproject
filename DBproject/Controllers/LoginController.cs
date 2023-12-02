using DBproject.BL.Auth;
using DBproject.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBproject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuth authBL;

        public LoginController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View(new ExecutorModel());
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Index(string Name, string Password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Authenticate(Name, Password);
                    return Redirect("/");
                } catch (Exception)
                {
                    ModelState.AddModelError("Name", "Имя или Пароль неверные");
                }
            }
            return View(new ExecutorModel() { Name=Name, Password=Password});
        }
        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> IndexOut()
        {
            await authBL.Logout();
            return Redirect("/");
        }
    }
}
