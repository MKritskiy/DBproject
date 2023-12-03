using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public interface ITeamDAL
    {
        Task<TeamModel> GetTeam(int teamid);
        Task<IEnumerable<TeamModel>> GetTeamsByExecutorId(int executorid);
        Task<IEnumerable<TeamModel>> GetAllTeams();
        Task<int> CreateTeam(TeamModel model);
        Task EnterInTeam(int executorid, int teamid);
        Task Delete(int id);
        Task Update(TeamModel model);
        Task<IEnumerable<ExecutorModel>> GetExecutorsInTeam(int teamid);
        Task<IEnumerable<TeamModel>> Search(string search);
    }
}
