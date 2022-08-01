using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WorkDiary.Entities;
using WorkDiary.Models;

namespace WorkDiary.Services
{
    public interface IAccountService
    {
        Task SignInAsync(LoginModel loginModel, HttpContext httpContext);
        Task RegisterAsync(RegisterModel registerModel, HttpContext httpContext);
    }
}