using DBproject.BL;
using DBproject.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBproject.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuth auth;
        public MainMenuViewComponent(IHttpContextAccessor httpContextAccessor, IAuth auth)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.auth = auth;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            string? executorId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            string? executorRole = null;
            if (executorId!=null)
                executorRole = await auth.GetRoleName(int.Parse(executorId));

            return View("Index", executorRole);
        }

    }
}
