using DBproject.BL.Auth;
using DBproject.BL.TaskList;
using DBproject.BL.Tasks;
using DBproject.BL.Team;
using DBproject.DAL.Models;
using DBproject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBproject.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ITeam teamBL;
        private readonly ITaskList taskListBL;
        IHttpContextAccessor httpContextAccessor;

        public TaskListController(ITeam teamBL, ITaskList taskListBL, IHttpContextAccessor httpContextAccessor)
        {
            this.teamBL = teamBL;
            this.taskListBL = taskListBL;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("/team/{teamid}/tasklistedit")]
        public async Task<IActionResult> Index(int teamid)
        {
            var team = await teamBL.GetTeam(teamid);
            if (team.TeamId == null)
                throw new Exception("Такой команды не существует");

            return View(new TaskListModel() { TeamId = teamid});
        }
        [HttpPost]
        [Route("/team/{teamid}/tasklistedit")]
        public async Task<IActionResult> Index(int teamid, TaskListModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TaskListId == null)
                    model.TeamId = teamid;
                await taskListBL.UpdateOrCreate(model);
                return Redirect("/team/"+teamid);

            }
            return View(model);
        }

        [HttpGet]
        [Route("/tasklists")]
        public async Task<IActionResult> IndexGetTaskLists()
        {
            var teams = await taskListBL.GetTaskLists();
            return View("TaskLists", teams);
        }

        [HttpGet]
        [Route("/team/{teamid}")]
        public async Task<IActionResult> IndexGetTeamTaskList(int teamid)
        {
            var tasklist = await taskListBL.GetTaskListsByTeamId(teamid);
            var team = await teamBL.GetTeam(teamid);
            
            return View("TeamTaskLists", new TeamViewModel() { TaskList = tasklist, Team=team});
        }

        [HttpGet]
        [Route("/team/{teamid}/tasklistdelete/{tasklistid}")]
        public async Task<IActionResult> IndexDelete(int teamid, int tasklistid)
        {
            await taskListBL.Delete(tasklistid);
            return Redirect("/team/" + teamid);
        }
    }
}
