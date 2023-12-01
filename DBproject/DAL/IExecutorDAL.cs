using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public interface IExecutorDAL
    {
        Task<ExecutorModel> GetExecutor(string name);
        Task<ExecutorModel> GetExecutor(int id);
        Task<int> CreateExecutor(ExecutorModel model);


    }
}
