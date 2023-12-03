using DBproject.BL.Auth;
using DBproject.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBproject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuth authBL;
        private readonly IHttpContextAccessor httpContextAccessor;
        public LoginController(IAuth authBL, IHttpContextAccessor httpContextAccessor)
        {
            this.authBL = authBL;
            this.httpContextAccessor = httpContextAccessor;
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

        [HttpGet]
        [Route("/giveadmin")]
        public async Task<IActionResult> IndexGiveAdmin()
        {
            string? executorId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            if (executorId !=null)
               await authBL.SetRole(int.Parse(executorId), Helper.AdminRoleID);
            return Redirect("/");
        }
    }
}
