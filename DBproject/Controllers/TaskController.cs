using DBproject.BL.Auth;
using DBproject.BL.TaskList;
using DBproject.BL.Tasks;
using DBproject.BL.Team;
using DBproject.DAL;
using DBproject.DAL.Models;
using DBproject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DBproject.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITask taskBL;
        private readonly ITaskList taskListBL;
        private readonly ITeam teamBL;
        private readonly IAuth authBL;
        IHttpContextAccessor httpContextAccessor;

        public TaskController(ITask taskBL, ITaskList taskListBL, IHttpContextAccessor httpContextAccessor, ITeam teamBL, IAuth authBL)
        {
            this.taskBL = taskBL;
            this.taskListBL = taskListBL;
            this.httpContextAccessor = httpContextAccessor;
            this.teamBL = teamBL;
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/team/{teamid}/tasklist/{tasklistid}/taskedit")]
        public async Task<IActionResult> Index(int teamid, int tasklistid)
        {
            var tasklist = await taskListBL.GetTaskList(tasklistid);
            if (tasklist.TaskListId == null)
                throw new Exception("Такого списка не существует");

            return View(new TaskViewModel() { CurrTaskListModel = tasklist, CurrTask = new TaskModel() {TaskListId= tasklist.TaskListId } });
        }
        [HttpPost]
        [Route("/team/{teamid}/tasklist/{tasklistid}/taskedit")]
        public async Task<IActionResult> Index(int teamid, int tasklistid, TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = model.CurrTask;
                string executorId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
                if (task == null)
                    throw new Exception("Задача не заполнена");
                task.TaskListId = tasklistid;
                if (task.StatusId == null)
                    task.StatusId = 1;
                task.SourceId = int.Parse(executorId);
                await taskBL.UpdateOrCreateTask(task);
                return Redirect("/team/"+teamid+"/tasklist/"+tasklistid);

            }
            return View(model);
        }

        [HttpGet]
        [Route("/team/{teamid}/tasklist/{tasklistid}")]
        public async Task<IActionResult> IndexGetTaskListTask(int teamid, int tasklistid, string? search, int? status, int? priority)
        {
            IEnumerable<TaskModel> tasks;
            if (search != null || status!=null || priority!=null)
            {
                ViewData["Search"] = search;
                tasks = await taskBL.Search(search, status, priority);
            }
            else
                tasks = await taskBL.GetAllByTaskListId(tasklistid);


            var tasklist = await taskListBL.GetTaskList(tasklistid);
            string? executorId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
            string? roleName = null;
            if (executorId != null)
                roleName = await authBL.GetRoleName(int.Parse(executorId));
            bool isInTeam = (await teamBL.GetTeamsByExecutorId(int.Parse(executorId ?? "0"))).Any(t => t.TeamId == teamid);
            return View("TaskListTasks", new TaskListViewModel() { Tasks = tasks, TaskList = tasklist, IsInTeam=isInTeam, UserRole=roleName});
        }

        [HttpGet]
        [Route("/team/{teamid}/tasklist/{tasklistid}/taskdelete/{taskid}")]
        public async Task<IActionResult> IndexDelete(int teamid, int tasklistid, int taskid)
        {
            await taskBL.Delete(taskid);
            return Redirect("/team/" + teamid + "/tasklist/" + tasklistid);
        }
    }
}
