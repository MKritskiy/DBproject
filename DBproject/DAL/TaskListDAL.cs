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

        public async Task UpdateTaskList(TaskListModel model)
        {
            string sql = @"Update task_list
                    set task_list_name = @Name
                    where task_list_id = @id; SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                Name = model.Name,
                id = model.TaskListId
            };
            await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}
