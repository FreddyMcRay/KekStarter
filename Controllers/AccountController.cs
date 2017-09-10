﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KekStarter.Models;
using KekStarter.ViewModels;

namespace KekStarter.Controllers
{
    [Route("api/")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private ApplicationContext _db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Incorrect login and(or) password");
                }
                else
                {
                    UserProfile userProfile = _db.UserProfile.FirstOrDefault(p => p.User.UserName == model.Login);
                    ResponseUserInfo responseUserInfo = new ResponseUserInfo { Id = userProfile.Id, Login = userProfile.User.UserName, Color = userProfile.Color, Language = userProfile.Language, Role = userProfile.Role};
                    return Ok(responseUserInfo);
                    //return Ok(model);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Login };
                //UserRole role = _db.UserRole.FirstOrDefault(p => p.Role == "User");
                UserProfile userProfile = new UserProfile { User = user, Language = "En", Color = "White", Role = "User" };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    //role.UserProfiles.Add(userProfile);
                    _db.UserProfile.Add(userProfile);
                    _db.SaveChanges();
                    return Ok("Register");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
    }
}