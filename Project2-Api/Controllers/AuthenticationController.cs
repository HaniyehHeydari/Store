﻿using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2_Api.Data.Domain;
using Project2_Api.Data.Entities;
using Shared.Models.Authentication;

namespace Project2_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _SignInManager;
        private readonly StoreDbContext _context; 
        public AuthenticationController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,

            StoreDbContext context)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
            _context = context;
        }
      

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Mobile);
            if (user != null)
            {
                return BadRequest("با این شماره همراه قبلا ثبت نام انجام شده است");
            }
            user = new AppUser
            {
                UserName = model.Mobile,
                PhoneNumber = model.Mobile,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] LoginRequestDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider sp)
        {
            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);
            _SignInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;
            var user = await _userManager.FindByNameAsync(login.Mobile);
            if (user == null)
            {
                return TypedResults.Problem("نام کاربری یا رمز عبور اشتباه است", statusCode: StatusCodes.Status401Unauthorized);
            }
            var result = await _SignInManager.PasswordSignInAsync(user, login.Password, isPersistent, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return TypedResults.Problem("نام کاربری یا رمز عبور اشتباه است", statusCode: StatusCodes.Status401Unauthorized);
            }
            return TypedResults.Empty;
        }

    }
}
