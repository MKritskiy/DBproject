using DBproject.BL;
using Microsoft.AspNetCore.Mvc;

namespace DBproject.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public MainMenuViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            return View("Index", isLoggedIn);
        }

    }
}
