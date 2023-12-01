using DBproject.DAL;
using DBproject.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Org.BouncyCastle.Crmf;
using System.Security.Claims;

namespace DBproject.BL
{
    public class Auth
    {
        public readonly IExecutorDAL executorDAL;
        public readonly IRoleDAL roleDAL;
        public readonly IEncrypt encrypt;
        public readonly IHttpContextAccessor httpContextAccessor;

        public Auth(IExecutorDAL executorDAL, IRoleDAL roleDAL, IEncrypt encrypt, IHttpContextAccessor httpContextAccessor)
        {
            this.executorDAL = executorDAL;
            this.roleDAL = roleDAL;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task Login(int id)
        {
            var executor = await executorDAL.GetExecutor(id);
            var role = await roleDAL.GetRole(executor.RoleId);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, executor.ExecutorName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.RoleName)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            await httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);
        }

        public async Task<int> Authenticate(string name, string password)
        {
            var executor = await executorDAL.GetExecutor(name);

            if (executor.ExecutorId != null && executor.ExecutorPassword == encrypt.HashPassword(password))
            {
                await Login(executor.ExecutorId ?? 0);


                return executor.ExecutorId ?? 0;
            }
            throw new Exception("Authorize Exception");
        }


        public async Task<int> CreateExecutor(ExecutorModel model)
        {
            model.ExecutorPassword = encrypt.HashPassword(model.ExecutorPassword);
            int id = await executorDAL.CreateExecutor(model);
            await Login(id);
            return id;


        }

        public async Task ValidateName(string name)
        {
            var executor = await executorDAL.GetExecutor(name);
            if (executor.ExecutorId != null)
                throw new Exception("Duplicate name exception");
        }

        public async Task Registration(ExecutorModel model)
        {
            await ValidateName(model.ExecutorName);
            await CreateExecutor(model);
            
        }
    }
}
