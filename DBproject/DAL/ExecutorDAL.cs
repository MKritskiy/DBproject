using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public class ExecutorDAL : IExecutorDAL
    {
        public async Task<ExecutorModel> GetExecutor(string name)
        {
            var result = await DbHelper.QueryAsync<ExecutorModel>(@"
                    select executor_id, executor_name, executor_email, executor_password, executor_phone_number, role_id_fk
                    from executor
                    where executor_name = @name", new { name = name });
            return result.FirstOrDefault() ?? new ExecutorModel();
        }

        public async Task<ExecutorModel> GetExecutor(int id)
        {
            var result = await DbHelper.QueryAsync<ExecutorModel>(@"
                    select executor_id, executor_name, executor_email, executor_password, executor_phone_number, role_id_fk
                    from executor
                    where executor_id = @id", new { id = id });
            return result.FirstOrDefault() ?? new ExecutorModel();
        }

        public async Task<int> CreateExecutor(ExecutorModel model)
        {
            string sql = @"insert into executor(executor_name, executor_email, executor_password, executor_phone_number, role_id_fk)
                    values(@ExectutorName ,@ExectutorEmail, @ExectutorPassword, @ExectutorPhoneNumber, @RoleId) returning executor_id";
            return await DbHelper.QueryScalarAsync<int>(sql,model);
        }
    }
}
