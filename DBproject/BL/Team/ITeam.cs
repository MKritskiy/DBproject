﻿using DBproject.DAL.Models;

namespace DBproject.BL.Team
{
    public interface ITeam
    {
        Task<TeamModel> GetTeam(int teamid);
        Task<IEnumerable<TeamModel>> GetTeamsByExecutorId(int executorid);
        Task<IEnumerable<TeamModel>> GetAllTeams();
        Task<int> CreateTeam(TeamModel model);
        Task EnterInTeam(int executorid, int teamid);
        Task Delete(int id);
        Task Update(TeamModel model);
        Task<int> UpdateOrCreate(TeamModel model);
        Task<IEnumerable<TeamModel>> Search(string search);


    }
}
