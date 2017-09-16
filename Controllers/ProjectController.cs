using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KekStarter.ViewModels;
using KekStarter.Models;

namespace KekStarter.Controllers
{
    [Route("api/")]
    public class ProjectController : Controller
    {
        private ApplicationContext _db;

        public TimeSpan elapsed = new TimeSpan();
        public DateTime date = DateTime.Now;

        public ProjectController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult CreateProject([FromBody] EditProfile model)
        {
            var project = new Project();
            DateTime localDate = DateTime.Now;
            // to...
            project.Title = null;
            project.Description = null;
            project.DateCreated = localDate.ToString();
            project.DateEnd = null;
            project.currentSum = 0;
            project.requiredSum = 0;
            project.urlImage = null;
            project.Tags = null;
            project.Targets = null;
            project.Sponsors = 0;
            project.progress = (project.currentSum / project.requiredSum) * 100;
            project.leftOver = 0;

            _db.Project.Add(project);
            _db.SaveChanges();
            return Ok(project);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult FollowProject([FromBody] FollowInfo model)
        {
            var followProject = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            followProject.UserProfiles.Add(_db.UserProfile.FirstOrDefault(p => p.Id == model.UserId));
            _db.Project.Update(followProject);
            _db.SaveChanges();
            return Ok(followProject);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult UnFollowProject([FromBody] FollowInfo model)
        {
            var followProject = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            followProject.UserProfiles.Remove(_db.UserProfile.FirstOrDefault(p => p.Id == model.UserId));
            _db.Project.Update(followProject);
            _db.SaveChanges();
            return Ok(followProject);

        }

        [HttpGet("[action]")]
        public IActionResult getProjects()
        {
            List<Project> projects = new List<Project>();
            projects = _db.Project.ToList();
            var frontProjects = new ProjectList
            {
                NewProjects = CheckNewProjects(projects),
                SuccessfulProjects = CheckSuccessfulProjects(projects)
            };
            return new ObjectResult(frontProjects);
        }

        public List<Project> CheckNewProjects(List<Project> projects)
        {
            var proj = new List<Project>();
            foreach (var project in projects)
            {
                elapsed = date.Subtract(Convert.ToDateTime(project.DateCreated));
                if (Convert.ToInt32(elapsed) < 7)
                {
                    proj.Add(project);
                }
            }
            return proj;
        }

        public List<Project> CheckSuccessfulProjects(List<Project> projects)
        {
            var proj = projects.FindAll(z => z.Status == true);
            return proj;
        }
    }
}