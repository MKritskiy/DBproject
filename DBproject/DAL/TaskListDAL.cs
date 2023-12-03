using DBproject.BL.Team;
using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public class TaskListDAL : ITaskListDAL
    {
        public async Task<int> CreateTaskList(TaskListModel model)
        {
            string sql = @"insert into task_list(task_list_name, team_id_fk)
                    values(@Name, @TeamId); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                Name = model.Name,
                TeamId = model.TeamId
            };
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<TaskListModel> GetTaskList(int id)
        {
            var result = await DbHelper.QueryAsync<TaskListModel>(@"
                    select task_list_id as TaskListId, task_list_name as Name, team_id_fk as TeamId
                    from task_list
                    where task_list_id = @id", new { id = id });
            return result.FirstOrDefault() ?? new TaskListModel();
        }

        public async Task<IEnumerable<TaskListModel>> GetTaskLists()
        {
            var result = await DbHelper.QueryAsync<TaskListModel>(@"
                    select task_list_id as TaskListId, task_list_name as Name, team_id_fk as TeamId
                    from task_list
                    ", new { });
            return result;
        }

        public async Task<IEnumerable<TaskListModel>> GetTaskListsByTeamId(int teamid)
        {
            var result = await DbHelper.QueryAsync<TaskListModel>(@"
                    select task_list_id as TaskListId, task_list_name as Name, team_id_fk as TeamId
                    from task_list AS tl JOIN team AS t ON tl.team_id_fk = t.team_id
                    where t.team_id = @id", new { id = teamid });
            return result;
        }

        public async Task Delete(int id)
        {
            string sql = @"delete from task_list where task_list_id = @id";
            await DbHelper.ExecuteAsync(sql, new { id = id });
        }
        public async Task Update(TaskListModel model)
        {
            string sql = "UPDATE task_list SET " +
                    "task_list_name = @Name " +
                    "WHERE task_list_id = @Id";

            var parameters = new
            {
                Id = model.TaskListId,
                Name = model.Name
            };

            await DbHelper.ExecuteAsync(sql, parameters);
        }
    }
}
