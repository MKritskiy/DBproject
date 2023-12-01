using DBproject.DAL.Models;
using DBproject.DAL;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DBproject.BL.Auth
{
    public interface IAuth
    {

        Task Login(int id);


        Task<int> Authenticate(string name, string password);



        Task<int> CreateExecutor(ExecutorModel model);


        Task ValidateName(string name);


        Task Register(string name, string password);

        Task Logout();


    }
}
