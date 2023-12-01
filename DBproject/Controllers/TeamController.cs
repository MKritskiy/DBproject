using DBproject.BL.Auth;
using DBproject.BL.Team;
using DBproject.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBproject.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeam teamBL;
        private readonly IAuth authBL;
        IHttpContextAccessor httpContextAccessor;

        public TeamController(ITeam teamBL, IAuth authBL, IHttpContextAccessor httpContextAccessor)
        {
            this.teamBL = teamBL;
            this.authBL = authBL;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("/teamedit")]
        public IActionResult Index()
        {
            return View(new TeamModel());
        }
        [HttpPost]
        [Route("/teamedit")]
        public async Task<IActionResult> Index(TeamModel model)
        {
            if (ModelState.IsValid)
            {
                string executorId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;

                if (model.TeamId != null)
                {
                    var team = await teamBL.GetTeam((int)model.TeamId);
                    if (team != null)
                    {
                        await teamBL.EnterInTeam(int.Parse(executorId), (int)model.TeamId);
                    }
                }

                int id = await teamBL.CreateTeam(model);
                if (id == 0)
                    throw new Exception("Команда не была создана");
                await teamBL.EnterInTeam(int.Parse(executorId), id);
                return Redirect("/");

            }
            return View(model);
        }

        [HttpGet]
        [Route("/teams")]
        public async Task<IActionResult> IndexGetTeams()
        {
            var teams = await teamBL.GetAllTeams();
            return View("Teams", teams);
        }

        [HttpGet]
        [Route("/my-teams")]
        public async Task<IActionResult> IndexGetMyTeams()
        {
            string executorId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var teams = await teamBL.GetTeamsByExecutorId(int.Parse(executorId));
            return View("MyTeams",teams);
        }
    }
}
