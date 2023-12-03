using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public interface ITaskDAL
    {
        Task<TaskModel> Get(int id);

        Task<IEnumerable<TaskModel>> GetAllByTaskListId(int tasklistid);

        Task<int> Create(TaskModel model);
        Task Update(TaskModel model);
        Task Delete(int id);
        Task<IEnumerable<TaskModel>> Search(string? search, int? status=null, int? priority=null);
    }
}
