using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public interface IRoleDAL
    {
        Task<RoleModel> GetRole(string name);
        Task<RoleModel> GetRole(int id);
        Task<int> CreateRole(RoleModel model);
        Task<string?> GetRoleName(int roleid);
    }
}
