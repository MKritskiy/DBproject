using DBproject.DAL.Models;
using System.Xml.Linq;

namespace DBproject.DAL
{
    public class RoleDAL : IRoleDAL
    {
        public async Task<RoleModel> GetRole(string name)
        {
            var result = await DbHelper.QueryAsync<RoleModel>(@"
                    select role_id, role_name
                    from role
                    where role_name = @name", new { name = name });
            return result.FirstOrDefault() ?? new RoleModel();
        }

        public async Task<RoleModel> GetRole(int id)
        {
            var result = await DbHelper.QueryAsync<RoleModel>(@"
                    select role_id, role_name
                    from role
                    where role_id = @id", new { id = id });
            return result.FirstOrDefault() ?? new RoleModel();
        }

        public async Task<int> CreateRole(RoleModel model)
        {
            string sql = @"insert into role(role_name)
                    values(@RoleName) returning role_id";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}
