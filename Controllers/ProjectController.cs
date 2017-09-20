using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KekStarter.ViewModels;
using KekStarter.Models;
using Microsoft.EntityFrameworkCore;

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
            proj.DateEnd = project.completionDate.Split("T").First();
            proj.currentSum = 0;
            proj.image = project.image;
            proj.CreateUserId = project.userId;
            proj.Sponsors = 0;
            elapsed = Convert.ToDateTime(proj.DateEnd).Subtract(Convert.ToDateTime(proj.DateCreated));
            proj.leftOver = elapsed.Days;
            proj.content = project.content;

            var tag = new Tag();
            for (int i = 0; i <= project.tags.Count - 1; i++)
            {
                tag = _db.Tag.FirstOrDefault(p => p.Name == project.tags[i]);
                if (tag == null)
                {
                    var bufTag = new Tag();
                    bufTag.Name = project.tags[i];
                    //bufTag.Projects.Add(proj);
                    _db.Tag.Add(bufTag);
                    tag = bufTag;
                }
                else
                {
                    //tag.Projects.Add(proj);
                    _db.Tag.Update(tag);
                }
                var bufProjectTag = new ProjectTag
                {
                    Tag = tag,
                    //Project = proj,
                    ProjectId = proj.Id
                };
                proj.Tags.Add(bufProjectTag);
                _db.InstructionTag.Add(bufProjectTag);
            }

            
            var target = new Target();
            foreach(var goals in project.finansalGoals)
            {
                target.cost = goals.cost;
                target.title = goals.title;
                target.Project = proj;
                proj.Targets.Add(target);
                proj.requiredSum += target.cost;
                _db.Step.Add(target);
            }

            if (proj.currentSum != 0)
            {
                proj.progress = (proj.currentSum / proj.requiredSum) * 100;
            }
            else
            {
                proj.currentSum = 0;
                proj.progress = 0;
            }
            return proj;
        }

        public void RefreshDate()
        {
            List<Project> projects = new List<Project>();
            projects = _db.Project.ToList();
            foreach (var project in projects)
            {
                elapsed = Convert.ToDateTime(project.DateEnd).Subtract(date);
                project.leftOver = elapsed.Days;
                _db.Project.Update(project);
            }
            _db.SaveChanges();
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
            RefreshDate();
            List<Project> projects = new List<Project>();
            projects = _db.Project.ToList();
            var tags = new List<Tag>();
            tags = _db.Tag.ToList();
            var frontProjects = new ProjectList
            {
                SuccessfulProjects = CheckSuccessfulProjects(projects),
                NewProjects = CheckNewProjects(projects),
                Tags = CheckTags(tags)
            };
            return new ObjectResult(frontProjects);
        }

        public List<string> CheckTags(List<Tag> tags)
        {
            var tag = new List<string>();
            foreach (var bufTag in tags)
            {
                tag.Add(bufTag.Name);
            }
            return tag;
        }

        public List<Project> CheckNewProjects(List<Project> projects)
        {
            var proj = new List<Project>();
            int i = 0;
            foreach (var project in projects)
            {
                elapsed = date.Subtract(Convert.ToDateTime(project.DateCreated));
                if (elapsed.Days < 1 && i < 4)
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

        [HttpGet("getProjects/{take}/{skip}/{property}/tag/{value}")]
        public List<Project> GetProjectsByTag(int take, int skip, string property, string type, string value)
        {
            var tag = _db.Tag.FirstOrDefault(p => p.Name == value);
            var tagProjects = _db.InstructionTag.ToList();
            var projects = _db.Project.ToList();

            var arr = tagProjects.FindAll(p => p.Tag == tag).ToList();
            var responseProjects = new List<Project>();
            foreach (var item in arr)
            {
                responseProjects.Add(projects.Find(p => p.Id == item.ProjectId));
            }
            return responseProjects.Skip(skip).Take(take).ToList();
        }

        [HttpGet("[action]/{id}/{userId}")]
        public IActionResult getProjectById(int id, int userId)
        {
            var projectController = new ProjectController(_db);
            var rating = _db.Rating.FirstOrDefault(p => (p.UserId == userId) && (p.ProjectId == id));
            projectController.RefreshDate();
            var project = _db.Project.FirstOrDefault(p => p.Id == id);
            foreach (var bufUser in project.UserProfiles)
            {
                if (bufUser.Id == userId)
                {
                    project.followed = true;
                }
                else
                {
                    project.followed = false;
                }
            }
            if (rating == null)
            {
                project.UserRating = 0;
            }
            else
            {
                project.UserRating = rating.Value;
            }
            return new ObjectResult(project);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult AddCommentary([FromBody] ViewModels.Commentary model)
        {
            var project = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            var commentary = new Models.Commentary();
            commentary.Content = model.Content;
            commentary.DateCreated = model.DateCreated;
            commentary.Project = project;
            commentary.UserProfile = _db.UserProfile.FirstOrDefault(p => p.Id == model.UserPofileId);
            _db.Commentary.Add(commentary);
            project.ProjectComments.Add(commentary);
            _db.Project.Update(project);
            _db.SaveChanges();
            return BadRequest("Ok");
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult AddRating([FromBody] ViewModels.Rating model)
        {
            var project = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            if (model.Value > 5 && model.Value < 0)
            {
                return BadRequest("Pashel nahuy pidor, naebat on reshil");
            }
            var rating = _db.Rating.FirstOrDefault(p => (p.ProjectId == model.ProjectId) && (p.UserId == model.UserPofileId));
            if (rating == null)
            {
                rating = new Models.Rating
                {
                    ProjectId = model.ProjectId,
                    UserId = model.UserPofileId,
                    Value = model.Value
                };
                project.Rating += model.Value;
                _db.Rating.Add(rating);
            }
            else
            {
                project.Rating -= rating.Value;
                project.Rating += model.Value;
                rating.Value = model.Value;
                _db.Rating.Update(rating);
            }
            _db.Project.Update(project);
            _db.SaveChanges();
            return Ok("Ok");
        }
    }
}