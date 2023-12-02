using DBproject.DAL;
using DBproject.DAL.Models;

namespace DBproject.BL.TaskList
{
    public class TaskList : ITaskList
    {
        private readonly ITaskListDAL taskListDAL;

        public TaskList(ITaskListDAL taskListDAL)
        {
            this.taskListDAL = taskListDAL;
        }

        public async Task<int> CreateTaskList(TaskListModel model)
        {
            return await taskListDAL.CreateTaskList(model);
        }

        public async Task<TaskListModel> GetTaskList(int id)
        {
            return await taskListDAL.GetTaskList(id);
        }

        public async Task<IEnumerable<TaskListModel>> GetTaskLists()
        {
            return await taskListDAL.GetTaskLists();
        }

        public async Task<IEnumerable<TaskListModel>> GetTaskListsByTeamId(int teamid)
        {
            return await taskListDAL.GetTaskListsByTeamId(teamid); 
        }

        public async Task UpdateTaskList(TaskListModel model)
        {
            await taskListDAL.UpdateTaskList(model);
        }

        public async Task UpdateOrCreateTaskList(TaskListModel model)
        {
            if (model.TaskListId != null)
                await UpdateTaskList(model);
            await CreateTaskList(model);
        }
    }
}
