using DBproject.DAL;
using DBproject.DAL.Models;

namespace DBproject.BL.Team
{
    public class Team : ITeam
    {
        private readonly ITeamDAL teamDAL;

        public Team(ITeamDAL teamDAL)
        {
            this.teamDAL = teamDAL;
        }

        public async Task<int> CreateTeam(TeamModel model)
        {
            return await teamDAL.CreateTeam(model);
        }

        public async Task EnterInTeam(int executorid, int teamid)
        {
            await teamDAL.EnterInTeam(executorid, teamid);
        }

        public async Task<IEnumerable<TeamModel>> GetAllTeams()
        {
            return await teamDAL.GetAllTeams();
        }

        public async Task<TeamModel> GetTeam(int teamid)
        {
            return await teamDAL.GetTeam(teamid);
        }

        public Task<IEnumerable<TeamModel>> GetTeamsByExecutorId(int executorid)
        {
            return teamDAL.GetTeamsByExecutorId(executorid);
        }
    }
}
