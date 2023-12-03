using DBproject.BL.Team;
using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public class TaskDAL : ITaskDAL
    {
        public async Task<int> Create(TaskModel model)
        {
            string sql = "insert into task (" +
                "task_name, task_description, task_priority, task_deadline_date, " +
                "status_id_fk, source_id_fk, task_list_id_fk)" +
                "values(@Name, @Description, @Priority, @DeadlineDate, @StatusId, @SourceId, @TaskListId); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                Name = model.Name,
                Description = model.Description,
                Priority = model.Priority,
                DeadlineDate = model.DeadlineDate,
                StatusId = model.StatusId,
                SourceId = model.SourceId,
                TaskListId = model.TaskListId,
            };

            return await DbHelper.QueryScalarAsync<int>(sql, model);

        }



        public async Task<TaskModel> Get(int id)
        {
            var result = await DbHelper.QueryAsync<TaskModel>(@"
                    select task_id as TaskId, task_name as Name, task_description as Description, 
                    task_priority as Priority, task_deadline_date as DeadlineDate,
                    status_id_fk as StatusId, source_id_fk as SourceId, task_list_id_fk as TaskListId
                    from task
                    where task_id = @id", new { id = id });
            return result.FirstOrDefault() ?? new TaskModel();
        }

        public async Task<IEnumerable<TaskModel>> GetAllByTaskListId(int tasklistid)
        {
            var result = await DbHelper.QueryAsync<TaskModel>(@"
                    select task_id as TaskId, task_name as Name, task_description as Description, 
                    task_priority as Priority, task_deadline_date as DeadlineDate,
                    status_id_fk as StatusId, source_id_fk as SourceId, task_list_id_fk as TaskListId
                    from task AS t JOIN task_list AS tl ON t.task_list_id_fk = tl.task_list_id
                    where tl.task_list_id = @id", new { id = tasklistid });
            return result;
        }
        public async Task Delete(int id)
        {
            string sql = @"delete from task where task_id = @id";
            await DbHelper.ExecuteAsync(sql, new { id = id });
        }
        public async Task Update(TaskModel model)
        {
            string sql = "UPDATE task SET " +
                    "task_name = @Name, " +
                    "task_description = @Description, " +
                    "task_priority = @Priority, " +
                    "task_deadline_date = @DeadlineDate, " +
                    "status_id_fk = @StatusId " +
                    "WHERE task_id = @Id";

            var parameters = new
            {
                Id = model.TaskId,
                Name = model.Name,
                Description = model.Description,
                Priority = model.Priority,
                DeadlineDate = model.DeadlineDate,
                StatusId = model.StatusId,
                TaskListId = model.TaskListId,
                SourceId = model.SourceId
            };

            await DbHelper.ExecuteAsync(sql, parameters);

        }
    }
}
