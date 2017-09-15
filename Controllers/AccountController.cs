using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KekStarter.Models;
using KekStarter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Login);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "You did not verify your email address");
                        return View(model);
                    }
                }
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Incorrect login and(or) password");
                }
                else
                {
                    DateTime thisDay = DateTime.Now;
                    UserProfile userProfile = _db.UserProfile.FirstOrDefault(p => p.User.UserName == model.Login);
                    userProfile.LastLogInDate = thisDay.ToString();
                    ResponseUserInfo responseUserInfo = new ResponseUserInfo { Id = userProfile.Id, Login = userProfile.User.UserName, Color = userProfile.Color, Language = userProfile.Language, Role = userProfile.Role, Token = userProfile.User.SecurityStamp};
                    _db.SaveChanges();
                    return Ok(responseUserInfo);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime thisDay = DateTime.Now;
                User user = new User { Email = model.Email, UserName = model.Login };
                UserProfile userProfile = new UserProfile { User = user, Language = "En", Color = "White", Role = "User", RegistrationDate = thisDay.ToString()  };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    await SendEmailAsync(model.Email, "Confirm your account", $"Confirm registration by clicking on the link: <a href='{ callbackUrl}'>link</a>");
                    _db.UserProfile.Add(userProfile);
                    _db.SaveChanges();
                    return Ok("Check your email to confirm your account");
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Ok("Goto /Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return Ok(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "game.malich@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("game.malich@gmail.com", "e5i9q94u6y");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult getUserById(int id)
        {
            var usProfile = _db.UserProfile.FirstOrDefault(p => p.Id == id);
            List<Project> projects = new List<Project>();
            projects = _db.Project.ToList();
            var proj = projects.FindAll(z => z.CreateUserId == id);
            var userInfo = new getUser { FirstName = usProfile.FirstName, SecondName = usProfile.SecondName, LastLogInDate = usProfile.LastLogInDate, RegistrationDate = usProfile.RegistrationDate, UrlPhoto = usProfile.UrlPhoto, Userprojects = proj };
            return new ObjectResult(userInfo);
        }
    }
}