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

        public async Task<int> Create(TaskListModel model)
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

        public async Task UpdateOrCreate(TaskListModel model)
        {
            if (model.TaskListId != null)
                await Update(model);
            else
                await Create(model);
        }

        public async Task Delete(int id)
        {
            await taskListDAL.Delete(id);
        }

        public async Task Update(TaskListModel model)
        {
            await taskListDAL.Update(model);
        }
        public async Task<IEnumerable<TaskListModel>> Search(string search)
        {
            return await taskListDAL.Search(search);
        }
    }
}
