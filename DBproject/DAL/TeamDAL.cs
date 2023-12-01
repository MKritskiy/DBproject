using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public class TeamDAL : ITeamDAL
    {
        public async Task<TeamModel> GetTeam(int teamid)
        {
            var result = await DbHelper.QueryAsync<TeamModel>(@"
                    select team_id as TeamId, team_name as Name
                    from team
                    where team_id = @id", new { id = teamid });
            return result.FirstOrDefault() ?? new TeamModel();
        }

        public async Task<IEnumerable<TeamModel>> GetTeamsByExecutorId(int executorid)
        {
            var result = await DbHelper.QueryAsync<TeamModel>(@"
                    select team_id as TeamId, team_name as Name
                    from team AS t JOIN team_executor AS te ON t.team_id = te.team_id_fk
                    where te.executor_id_fk = @id", new { id = executorid });
            return result;
        }

        public async Task<IEnumerable<TeamModel>> GetAllTeams()
        {
            var result = await DbHelper.QueryAsync<TeamModel>(@"
                    select team_id as TeamId, team_name as Name
                    from team
                    ", new { });
            return result;
        }

        public async Task<int> CreateTeam(TeamModel model)
        {
            string sql = @"insert into team(team_name)
                    values(@Name); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                Name = model.Name,
            };
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task EnterInTeam(int executorid, int teamid)
        {
            string sql = @"insert into team_executor(team_id_fk, executor_id_fk)
                    values(@TeamId, @ExecutorId); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                TeamId = teamid,
                ExecutorId= executorid
            };
            await DbHelper.ExecuteAsync(sql, parameters);
        }
    }
}
