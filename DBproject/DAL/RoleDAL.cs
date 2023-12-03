using DBproject.DAL.Models;
using System.Xml.Linq;

namespace DBproject.DAL
{
    public class RoleDAL : IRoleDAL
    {
        public async Task<RoleModel> GetRole(string name)
        {
            var result = await DbHelper.QueryAsync<RoleModel>(@"
                    select role_id as RoleId, role_name as Name
                    from role
                    where role_name = @name", new { name = name });
            return result.FirstOrDefault() ?? new RoleModel();
        }

        public async Task<RoleModel> GetRole(int id)
        {
            var result = await DbHelper.QueryAsync<RoleModel>(@"
                    select role_id as RoleId, role_name as Name
                    from role
                    where role_id = @id", new { id = id });
            return result.FirstOrDefault() ?? new RoleModel();
        }

        public async Task<string?> GetRoleName(int roleid)
        {
            var result = await DbHelper.QueryAsync<string>(@"CALL GetRole(@id, @role_name); SELECT @role_name as Name;", new { id = roleid });
            return result.FirstOrDefault();
        }

        public async Task<int> CreateRole(RoleModel model)
        {
            string sql = @"insert into role(role_name)
                    values(@RoleName); SELECT LAST_INSERT_ID();";
            var parameters = new
            {
                RoleName = model.Name,
            };
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}
