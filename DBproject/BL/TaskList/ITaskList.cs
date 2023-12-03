using DBproject.DAL.Models;

namespace DBproject.BL.TaskList
{
    public interface ITaskList
    {
        Task<TaskListModel> GetTaskList(int id);
        Task<IEnumerable<TaskListModel>> GetTaskListsByTeamId(int teamid);
        Task<IEnumerable<TaskListModel>> GetTaskLists();
        Task<int> Create(TaskListModel model);
        Task Update(TaskListModel model);
        Task UpdateOrCreate(TaskListModel model);
        Task Delete(int id);
        Task<IEnumerable<TaskListModel>> Search(string search);
    }
}
