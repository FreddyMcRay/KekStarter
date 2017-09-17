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
        public DateTime localDate = DateTime.Now;

        public ProjectController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult CreateProject([FromBody] CreateProjectInfo model)
        {
            _db.Project.Add(FillingFields(model));
            _db.SaveChanges();
            return new ObjectResult(model);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult UpdateProject([FromBody] CreateProjectInfo model)
        {
            var project = _db.Project.FirstOrDefault(p => p.Id == model.id);
            _db.Project.Update(FillingFields(model));
            _db.SaveChanges();
            return new ObjectResult(project);
        }

        public Project FillingFields(CreateProjectInfo project)
        {
            var proj = new Project();
            proj.Title = project.title;
            proj.Description = project.description;
            proj.DateCreated = localDate.ToString();
            proj.DateEnd = project.completionDate;
            proj.currentSum = 0;
            proj.requiredSum = project.totalCost;
            proj.image = project.image;
            proj.CreateUserId = project.userId;
            //proj.Targets = project.finansalGoals;
            proj.Sponsors = 0;
            //project.progress = (project.currentSum / project.requiredSum) * 100;
            //project.leftOver = Convert.ToInt32(date.Subtract(Convert.ToDateTime(project.DateCreated)));

            var tag = new Tag();
            for (int i = 0; i <= project.tags.Count - 1; i++)
            {
                tag = _db.Tag.FirstOrDefault(p => p.Name == project.tags[i]);
                if (tag == null)
                {
                    var bufTag = new Tag();
                    bufTag.Name = project.tags[i];
                    bufTag.Projects.Add(proj);
                    _db.Tag.Add(bufTag);
                }
                else
                {
                    tag.Projects.Add(proj);
                    _db.Tag.Update(tag);
                }
            }
            var bufProjectTag = new ProjectTag
            {
                Tag = tag,
                Project = proj,
            };
            proj.Tags.Add(bufProjectTag);
            _db.InstructionTag.Add(bufProjectTag);

            return proj;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult FollowProject([FromBody] FollowInfo model)
        {
            var followProject = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            followProject.UserProfiles.Add(_db.UserProfile.FirstOrDefault(p => p.Id == model.UserId));
            _db.Project.Update(followProject);
            _db.SaveChanges();
            return new ObjectResult(followProject);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult UnFollowProject([FromBody] FollowInfo model)
        {
            var followProject = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            followProject.UserProfiles.Remove(_db.UserProfile.FirstOrDefault(p => p.Id == model.UserId));
            _db.Project.Update(followProject);
            _db.SaveChanges();
            return new ObjectResult(followProject);

        }

        [HttpGet("[action]")]
        public IActionResult GetProjects()
        {
            List<Project> projects = new List<Project>();
            projects = _db.Project.ToList();
            var frontProjects = new ProjectList
            {
                SuccessfulProjects = CheckSuccessfulProjects(projects),
                NewProjects = CheckNewProjects(projects)
            };
            return new ObjectResult(frontProjects);
        }

        public List<Project> CheckNewProjects(List<Project> projects)
        {
            var proj = new List<Project>();
            int i = 0;
            foreach (var project in projects)
            {
                elapsed = date.Subtract(Convert.ToDateTime(project.DateCreated));
                if (elapsed.Days < 3 && i < 4)
                {
                    proj.Add(project);
                    i++;
                }
            }
            return proj;
        }

        public List<Project> CheckSuccessfulProjects(List<Project> projects)
        {
            var proj = projects.FindAll(z => z.Status == true);
            var reProject = new List<Project>();
            int i = 0;
            foreach (var project in proj)
            {
                if (i < 4)
                {
                    i++;
                    reProject.Add(project);
                }
            }
            return proj;
        }
    }
}