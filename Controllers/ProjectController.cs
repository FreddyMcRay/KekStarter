﻿using System;
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
            return new ObjectResult(_db.Project.Last().Id);
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
            int helpIdTag = 0;
            int j = 1;
            for (int i = 0; i <= project.tags.Count - 1; i++)
            {
                tag = _db.Tag.FirstOrDefault(p => p.Name == project.tags[i]);
                if (tag == null)
                {
                    var bufTag = new Tag();
                    bufTag.Name = project.tags[i];
                    helpIdTag = _db.Tag.ToList().Last().Id + j;
                    _db.Tag.Add(bufTag);
                    tag = bufTag;
                    j++;
                }
                else
                {
                    helpIdTag = tag.Id;
                    _db.Tag.Update(tag);
                }
                var bufProjectTag = new ProjectTag
                {
                    Tag = tag,
                    ProjectId = _db.Project.ToList().Last().Id + 1,
                    IdTags = helpIdTag
                };
                proj.Tags.Add(bufProjectTag);
                _db.InstructionTag.Add(bufProjectTag);
            }
            
            //
            foreach(var goals in project.finansalGoals)
            {
                var target = new Target();
                target.projectId = _db.Project.ToList().Last().Id + 1;
                target.cost = goals.cost;
                target.title = goals.title;
                target.IsCompleted = false;
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
            var display = new Display();
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
            var tags = _db.InstructionTag.ToList();
            var tagsString = new List<string>();
            tags = tags.FindAll(p => p.ProjectId == id);
            foreach (var tag in tags)
            {
                var bufTag = _db.Tag.FirstOrDefault(p => p.Id == tag.IdTags);
                if (bufTag != null)
                {
                    tagsString.Add(bufTag.Name);
                }
                else
                {
                    tagsString.Add("");
                }
                
            }

            var goals = new List<FinansalGoal>();
            foreach (var tar in _db.Step.ToList().FindAll(p => p.projectId == id))
            {
                var finansalGoal = new FinansalGoal();
                finansalGoal.cost = tar.cost;
                finansalGoal.isCompleted = tar.IsCompleted;
                finansalGoal.title = tar.title;
                goals.Add(finansalGoal);
            }
            

            display.project = project;
            display.tags = tagsString;
            display.finansalGoal = goals;
            return new ObjectResult(display);
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

        //Begin Commentary

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult addCommentInProject([FromBody] ViewModels.Commentary model)
        {
            var userProfile = _db.UserProfile.ToList().FirstOrDefault(p => p.Id == model.userProfileMini.id);
            var project = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            var commentary = new Models.Commentary();
            AddCommentaryAction(model, project, commentary, userProfile);
            return Ok("Ok");
        }

        public Models.Commentary ContructorCommentary(ViewModels.Commentary model, Models.Commentary commentary, Project project)
        {
            commentary.Content = model.Content;
            commentary.DateCreated = model.DateCreated;
            commentary.Project = project;
            commentary.UserProfile = _db.UserProfile.FirstOrDefault(p => p.Id == model.userProfileMini.id);
            return commentary;
        }

        public void AddCommentaryAction(ViewModels.Commentary model, Project project, Models.Commentary commentary, UserProfile userProfile)
        {
            if (Validation(userProfile))
            {
                commentary = ContructorCommentary(model, commentary, project);
                AddCommentary(project, commentary);
            }
            BadRequest("Error. 401 Unauthorized.");
        }

        public void AddCommentary(Project project, Models.Commentary commentary)
        {
            _db.Commentary.Add(commentary);
            project.ProjectComments.Add(commentary);
            UpdateProjectDB(project);
        }
        
        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult removeCommentInProject([FromBody] ViewModels.RemoveComment model)
        {
            var userProfile = _db.UserProfile.ToList().FirstOrDefault(p => p.Id == model.UserId);
            var project = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            var commentary = _db.Commentary.ToList().FirstOrDefault(p => p.Id == model.CommentaryId);
            RemoveCommentaryAction(model, project, commentary, userProfile);
            return Ok("Ok");
        }

        public bool Validation(UserProfile userProfile)
        {
            if (userProfile.Role == "User" || userProfile.Role == "AuthUser" || userProfile.Role == "Admin")
            {
                return true;
            }
            return false;
        }

        public void RemoveCommentaryAction(ViewModels.RemoveComment model, Project project, Models.Commentary commentary, UserProfile userProfile)
        {
            if (Validation(userProfile))
            {
                RemoveCommentary(project, commentary);
            }
            BadRequest("Error. 401 Unauthorized.");
        }

        public void RemoveCommentary(Project project, Models.Commentary commentary)
        {
            _db.Commentary.Remove(commentary);
            project.ProjectComments.Remove(commentary);
            UpdateProjectDB(project);
        }

        public void UpdateProjectDB(Project project)
        {
            _db.Project.Update(project);
            _db.SaveChanges();
        }

        [HttpGet("[action]/{id}/{userId}")]
        public IActionResult getCommentsByProjects(int projectId, int skip, int take)
        {
            var commentaryes = new List<ViewModels.Commentary>();
            return new ObjectResult(commentaryes = ViewCommentaryes(projectId, commentaryes));
        }

        public List<ViewModels.Commentary> ViewCommentaryes(int projectId, List<ViewModels.Commentary> commentaryes)
        {
            return commentaryes = View(projectId);
        }

        public List<ViewModels.Commentary> View(int projectId)
        {
            var commentaryes = _db.Commentary.ToList().FindAll(p => p.Project.Id == projectId);
            var viewCommentaryes = new List<ViewModels.Commentary>();
            foreach (var comment in commentaryes)
            {
                var bufComment = new ViewModels.Commentary();
                FillCommentInfo(comment, bufComment);
                FillUserMiniInfo(comment, bufComment);
            }
            return viewCommentaryes;
        }

        public ViewModels.Commentary FillCommentInfo(Models.Commentary comment, ViewModels.Commentary bufComment)
        {
            bufComment.Id = comment.Id;
            bufComment.ProjectId = comment.Project.Id;
            bufComment.Content = comment.Content;
            bufComment.DateCreated = comment.DateCreated;
            return bufComment;
        }

        public ViewModels.Commentary FillUserMiniInfo(Models.Commentary comment, ViewModels.Commentary bufComment)
        {
            bufComment.userProfileMini.id = comment.UserProfile.Id;
            bufComment.userProfileMini.firstName = comment.UserProfile.FirstName;
            bufComment.userProfileMini.secondName = comment.UserProfile.SecondName;
            bufComment.userProfileMini.urlPhoto = comment.UserProfile.UrlPhoto;
            return bufComment;
        }

        //End Commentary
    }
}