using DBproject.DAL.Models;

namespace DBproject.BL.Tasks
{
    public interface ITask
    {
        Task<TaskModel> Get(int id);

        Task<IEnumerable<TaskModel>> GetAllByTaskListId(int tasklistid);

        Task<int> Create(TaskModel model);
        Task Update(TaskModel model);
        Task Delete(int id);

        Task UpdateOrCreateTask(TaskModel model);
        Task<IEnumerable<TaskModel>> Search(string? search, int? status, int? priority);
    }
}
