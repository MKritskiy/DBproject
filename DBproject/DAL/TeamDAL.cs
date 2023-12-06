using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public class TeamDAL : ITeamDAL
    {
        public async Task<TeamModel> GetTeam(int teamid)
        {
            var result = await DbHelper.QueryAsync<TeamModel>(@"
                    select team_id as TeamId, team_name as Name, team_pass as Pass
                    from team
                    where team_id = @id", new { id = teamid });
            return result.FirstOrDefault() ?? new TeamModel();
        }

        public async Task<IEnumerable<TeamModel>> GetTeamsByExecutorId(int executorid)
        {
            var result = await DbHelper.QueryAsync<TeamModel>(@"
                    select team_id as TeamId, team_name as Name, team_pass as Pass
                    from team AS t JOIN team_executor AS te ON t.team_id = te.team_id_fk
                    where te.executor_id_fk = @id", new { id = executorid });
            return result;
        }

        public async Task<IEnumerable<TeamModel>> GetAllTeams()
        {
            var result = await DbHelper.QueryAsync<TeamModel>(@"
                    select team_id as TeamId, team_name as Name, team_pass as Pass
                    from team
                    ", new { });
            return result;
        }

        public async Task<int> CreateTeam(TeamModel model)
        {
            string sql = @"insert into team(team_name, team_pass)
                    values(@Name, @Pass); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                Name = model.Name,
                Pass = model.Pass
            };
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<ExecutorModel>> GetExecutorsInTeam(int teamid)
        {
            string sql = @"SELECT executor_id AS ExecutorId, executor_name AS Name, executor_email AS Email, 
                    executor_password AS Password, executor_phone_number AS PhoneNumber, role_id_fk AS RoleId
                    FROM executor AS e JOIN team_executor AS te ON te.executor_id_fk=e.executor_id
                    WHERE te.team_id_fk=@id";
            var parameters = new { id = teamid };
            return await DbHelper.QueryAsync<ExecutorModel>(sql, parameters);
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

        public async Task Delete(int id)
        {
            string sql = @"delete from team where team_id = @id";
            await DbHelper.ExecuteAsync(sql, new { id = id });
        }
        public async Task Update(TeamModel model)
        {
            string sql = "UPDATE team SET " +
                    "team_name = @Name, " +
                    "team_pass = @Pass " +
                    "WHERE team_id = @Id";

            var parameters = new
            {
                Id = model.TeamId,
                Name = model.Name,
                Pass = model.Pass
            };

            await DbHelper.ExecuteAsync(sql, parameters);
        }

        public async Task<IEnumerable<TeamModel>> Search(string search)
        {

            string sql = @"select team_id as TeamId, team_name as Name, team_pass as Pass
                    from team
                    where team_name LIKE @search;";
            var parameters = new { search = '%'+search+'%' };
            return await DbHelper.QueryAsync<TeamModel>(sql, parameters);
        }
    }
}
