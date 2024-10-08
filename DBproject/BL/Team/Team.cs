﻿using DBproject.DAL;
using DBproject.DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DBproject.BL.Team
{
    public class Team : ITeam
    {
        private readonly ITeamDAL teamDAL;
        private readonly IHttpContextAccessor httpContextAccessor;

        public Team(ITeamDAL teamDAL, IHttpContextAccessor httpContextAccessor)
        {
            this.teamDAL = teamDAL;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<int> CreateTeam(TeamModel model)
        {
            return await teamDAL.CreateTeam(model);
        }



        public async Task EnterInTeam(int executorid, int teamid)
        {
            if ((await GetTeamsByExecutorId(executorid)).Any(t => t.TeamId == teamid))
                return;
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

        public async Task<IEnumerable<TeamModel>> GetTeamsByExecutorId(int executorid)
        {
            return await teamDAL.GetTeamsByExecutorId(executorid);
        }


        public async Task Delete(int id)
        {
            await teamDAL.Delete(id);
        }
        public async Task Update(TeamModel model)
        {
            await teamDAL.Update(model);
        }

        public async Task<int> UpdateOrCreate(TeamModel model)
        {
            if (model.TeamId != null)
            {
                await teamDAL.Update(model);
                return (int)model.TeamId;
            }
            else
            {
                int teamid=  await teamDAL.CreateTeam(model);
                string? executorId = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
                if (executorId != null)
                    await EnterInTeam(int.Parse(executorId), teamid);
                return teamid;

            }

        }

        public async Task<IEnumerable<TeamModel>> Search(string search)
        {
            return await teamDAL.Search(search);
        }
    }
}

