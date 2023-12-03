using DBproject.DAL;
using DBproject.DAL.Models;

namespace DBproject.BL.Tasks
{
    public class TaskBL : ITask
    {
        private readonly ITaskDAL taskDAL;

        public TaskBL(ITaskDAL taskDAL)
        {
            this.taskDAL = taskDAL;
        }

        public async Task<int> Create(TaskModel model)
        {
            
            return await taskDAL.Create(model);
        }

        public async Task Delete(int id)
        {
            await taskDAL.Delete(id);
        }

        public async Task<TaskModel> Get(int id)
        {
            return await taskDAL.Get(id);
        }

        public async Task<IEnumerable<TaskModel>> GetAllByTaskListId(int tasklistid)
        {
            return await taskDAL.GetAllByTaskListId(tasklistid);
        }

        public async Task Update(TaskModel model)
        {
            await taskDAL.Update(model);
        }

        public async Task UpdateOrCreateTask(TaskModel model)
        {
            if (model.TaskId != null)
                await Update(model);
            else
                await Create(model);
        }
    }
}
