
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WorkDiary.Entities;
using WorkDiary.Enums;
using WorkDiary.Models;

namespace WorkDiary.Services
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task RegisterAsync(RegisterModel registerModel, HttpContext httpContext)
        {
            var userInDb = await _userService.GetByUserNameAsync(registerModel.UserName);

            if (userInDb != null)
            {
                throw new Exception("User already exists");
            }

            var passwordHasher = new PasswordHasher<User>();

            var newUser = new User
            {
                UserName = registerModel.UserName,
                Password = passwordHasher.HashPassword(new User(), registerModel.Password),
                Role = (int)Roles.User
            };

            await _userService.CreateAsync(newUser);

            await httpContext.SignInAsync(GetClaimsPrincipal(newUser));
        }

        public async Task SignInAsync(LoginModel loginModel, HttpContext httpContext)
        {
            var userInDb = await _userService.GetByUserNameAsync(loginModel.UserName);

            if (userInDb == null)
            {
                throw new Exception("User is not found");
            }

            var passwordHasher = new PasswordHasher<User>();

            var passwordVerification = passwordHasher.VerifyHashedPassword(new User(), userInDb.Password, loginModel.Password);

            if (passwordVerification != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid password");
            }

            await httpContext.SignInAsync(GetClaimsPrincipal(userInDb));
        }

        public ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, nameof(user.Role))
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}

