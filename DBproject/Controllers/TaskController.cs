using DBproject.BL.TaskList;
using DBproject.BL.Tasks;
using DBproject.BL.Team;
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
        IHttpContextAccessor httpContextAccessor;

        public TaskController(ITask taskBL, ITaskList taskListBL, IHttpContextAccessor httpContextAccessor)
        {
            this.taskBL = taskBL;
            this.taskListBL = taskListBL;
            this.httpContextAccessor = httpContextAccessor;
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

                if (task.TaskListId == null)
                    task.TaskListId = tasklistid;
                task.StatusId = 1;
                task.SourceId = int.Parse(executorId);
                await taskBL.UpdateOrCreateTask(task);
                return Redirect("/team/"+teamid+"/tasklist/"+tasklistid);

            }
            return View(model);
        }

        [HttpGet]
        [Route("/team/{teamid}/tasklist/{tasklistid}")]
        public async Task<IActionResult> IndexGetTaskListTask(int teamid, int tasklistid)
        {
            var tasklist = await taskListBL.GetTaskList(tasklistid);
            var tasks = await taskBL.GetAllByTaskListId(tasklistid);
            
            return View("TaskListTasks", new TaskListViewModel() { Tasks = tasks, TaskList = tasklist});
        }
    }
}
