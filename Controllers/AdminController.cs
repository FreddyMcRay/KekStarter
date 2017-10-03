using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using KekStarter.Models;
using Microsoft.EntityFrameworkCore;
using KekStarter.ViewModels;

namespace KekStarter.Controllers
{
[Route("api/")] 
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;

    private ApplicationContext _db;

    public AdminController(UserManager<User> userManager, ApplicationContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    [HttpGet("[action]")]
    public IActionResult GetAllUsers()
    {
        var allUsers = new List<GetAllUsers>();
        var users = _db.UserProfile.ToList();
        return new ObjectResult(ListUsers(users));
    }

    public List<GetAllUsers> ListUsers(List<UserProfile> users)
    {
        var allUsers = new List<GetAllUsers>();
        var bufUser = new User();
        foreach (var user in users)
        {
            bufUser = _db.UserProfile.Include(p => p.User).FirstOrDefault(t => t.Id == user.Id).User;
            allUsers.Add(FactoryUser(user, bufUser));
        }
        return allUsers;
    }

    public bool CheckUser(UserProfile user)
    {
        var confirmation = _db.Confirmation.FirstOrDefault(p => p.UserId == user.Id);
        if (confirmation != null)
        {
            return true;
        }
        return false;
    }

    public string getScan(bool flag, UserProfile user)
    {
        if (flag == true)
        {
            return _db.Confirmation.FirstOrDefault(p => p.UserId == user.Id).Scan;
        }
        return "";
    }

    public GetAllUsers FactoryUser(UserProfile user, User bufUser)
    {
        bool flag = CheckUser(user);
        var userToAdmin = new GetAllUsers
        {
            Id = user.Id,
            image = user.UrlPhoto,
            isBlocked = bufUser.LockoutEnabled,
            role = user.Role,
            onCheck = flag,
            scanImage = getScan(flag, user),
            email = bufUser.Email,
            userName = bufUser.UserName,
        };
        return userToAdmin;
    }

        [HttpPost("[action]")]
        public IActionResult sendConfirm([FromBody] Scan model)
        {
            var conform = new Confirmation()
            {
                Scan = model.scanImage,
                UserId = model.userId,
                Status = true
            };
            _db.Confirmation.Add(conform);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost("[action]")]
    [AllowAnonymous]
    public IActionResult RemoveUser([FromBody] List<GetAllUsers> model)
    {
        RemoveAction(model);
        _db.SaveChanges();
        return Ok();
    }

    public void RemoveAction(List<GetAllUsers> model)
    {
        foreach (var user in model)
        {
            var userProf = _db.UserProfile.FirstOrDefault(p => p.Id == user.Id);
            _db.UserProfile.Remove(userProf);
                //_db.Commentary.Remove(_db.Commentary.FirstOrDefault(p => p.IdUserProfile == user.Id));
                if (_db.Project.FirstOrDefault(p => p.CreateUserId == user.Id) != null)
                {
                    _db.Project.Remove(_db.Project.FirstOrDefault(p => p.CreateUserId == user.Id));
                }
        }
    }

    [HttpPost("[action]")]
    public virtual async Task<IdentityResult> LockUserAccount([FromBody] List<GetAllUsers> users)
    {
        IdentityResult result = new IdentityResult();
        foreach (var user in users)
        {
            var tmpUser = _db.UserProfile.Include(p => p.User).FirstOrDefault(p => p.Id == user.Id).User;
            result = await _userManager.SetLockoutEnabledAsync(tmpUser, true);
            if (result.Succeeded)
            {
                result = await _userManager.SetLockoutEndDateAsync(tmpUser, DateTimeOffset.MaxValue);
            }
        }
        return result;
    }

    [HttpPost("[action]")]
    public virtual async Task<IdentityResult> UnlockUserAccount([FromBody] List<GetAllUsers> users)
    {
        IdentityResult result = new IdentityResult();
        foreach (var user in users)
        {
            var tmpUser = _db.UserProfile.Include(p => p.User).FirstOrDefault(p => p.Id == user.Id).User;
            result = await _userManager.SetLockoutEnabledAsync(tmpUser, false);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(tmpUser);
            }
        }
        return result;
    }

    [HttpGet("[action]/{id}")]
    public IActionResult ConfirmUser(int id)
    {
        _db.Confirmation.Remove(_db.Confirmation.FirstOrDefault(p => p.UserId == id));
        var user = _db.UserProfile.FirstOrDefault(p => p.Id == id);
        user.Role = "AuthUser";
        _db.UserProfile.Update(user);
        _db.SaveChanges();
        return Ok();
    }

    [HttpGet("[action]/{id}")]
    public IActionResult UnConfirmUser(int id)
    {
        _db.Confirmation.Remove(_db.Confirmation.FirstOrDefault(p => p.UserId == id));
        _db.SaveChanges();
        return Ok();
    }

}
}