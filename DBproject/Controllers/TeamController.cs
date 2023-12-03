using DBproject.BL.Auth;
using DBproject.BL.Team;
using DBproject.DAL.Models;
using DBproject.ViewModels;
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
                string? executorId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;

                int id = await teamBL.UpdateOrCreate(model);
                if (id == 0)
                    throw new Exception("Команда не была создана");
  
                return Redirect("/my-teams");

            }
            return View(model);
        }

        [HttpGet]
        [Route("/teams")]
        public async Task<IActionResult> IndexGetTeams(string? search)
        {
            List<TeamModel> teams;
            if (search != null)
            {
                ViewData["Search"] = search;
                teams = (await teamBL.Search(search)).ToList();

            }
            else
                teams = (await teamBL.GetAllTeams()).ToList();
            string? executorId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            string? userRole = null;
            if (executorId !=null)
                userRole = await authBL.GetRoleName(int.Parse(executorId));
            List<TeamViewModel> teamsVM = new List<TeamViewModel>();

            for (int i = 0; i<teams.Count(); i++)
            {
                bool isInTeam = (await teamBL.GetTeamsByExecutorId(int.Parse(executorId ?? "0"))).Any(t => t.TeamId == teams[i].TeamId);
                teamsVM.Add(new TeamViewModel { Team = teams[i], IsInTeam=isInTeam, UserRole=userRole });
                
            }
            return View("Teams", teamsVM);
        }

        [HttpGet]
        [Route("/my-teams")]
        public async Task<IActionResult> IndexGetMyTeams(string? search)
        {
            IEnumerable<TeamModel> teams;
            if (search != null)
            {
                ViewData["Search"] = search;
                teams = (await teamBL.Search(search)).ToList();

            }
            string executorId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            teams = await teamBL.GetTeamsByExecutorId(int.Parse(executorId));
            return View("MyTeams",teams);
        }

        [HttpGet]
        [Route("/teamdelete/{teamid}")]
        public async Task<IActionResult> IndexDelete(int teamid)
        {
            await teamBL.Delete(teamid);
            return Redirect("/my-teams");
        }

        [HttpGet]
        [Route("/teamenter/{teamid}")]
        public async Task<IActionResult> EnterInTeam(int teamid)
        {
            string executorId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            await teamBL.EnterInTeam(int.Parse(executorId), teamid);

            return Redirect("/my-teams");
        }
    }
}
