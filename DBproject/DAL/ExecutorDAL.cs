using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public class ExecutorDAL : IExecutorDAL
    {
        public async Task<ExecutorModel> GetExecutor(string name)
        {
            var result = await DbHelper.QueryAsync<ExecutorModel>(@"
                    SELECT executor_id AS ExecutorId, executor_name AS Name, executor_email AS Email, 
                    executor_password AS Password, executor_phone_number AS PhoneNumber, role_id_fk AS RoleId
                    FROM executor
                    WHERE executor_name = @name", new { name=name });
            return result.FirstOrDefault() ?? new ExecutorModel();
        }

        public async Task<ExecutorModel> GetExecutor(int id)
        {
            var result = await DbHelper.QueryAsync<ExecutorModel>(@"
                SELECT executor_id AS ExecutorId, executor_name AS Name, executor_email AS Email, 
                executor_password AS Password, executor_phone_number AS PhoneNumber, role_id_fk AS RoleId
                FROM executor
                WHERE executor_id = @id", new { id=id });
            return result.FirstOrDefault() ?? new ExecutorModel();
        }

        public async Task<int> CreateExecutor(ExecutorModel model)
        {
            string sql = @"insert into executor(executor_name, executor_email, executor_password, executor_phone_number, role_id_fk)
                    values(@Name, @Email, @Password, @PhoneNumber, @RoleId); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                RoleId = model.RoleId
            };
            return await DbHelper.QueryScalarAsync<int>(sql, parameters);
        }
    }
}
