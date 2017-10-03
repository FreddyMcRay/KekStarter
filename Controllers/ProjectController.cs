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
                    if (_db.Tag.ToList().Count != 0)
                    {
                        helpIdTag = _db.Tag.ToList().Last().Id + j;
                    }
                    else
                    {
                        helpIdTag = j;
                    }
                    _db.Tag.Add(bufTag);
                    tag = bufTag;
                    j++;
                }
                else
                {
                    helpIdTag = tag.Id;
                    _db.Tag.Update(tag);
                }
                int idProj = 0;
                if (_db.Project.ToList().Count != 0)
                {
                    idProj = _db.Project.ToList().Last().Id + 1;
                }
                else
                {
                    idProj = 0;
                }
                var bufProjectTag = new ProjectTag
                {
                    Tag = tag,
                    ProjectId = idProj,
                    IdTags = helpIdTag
                };
                proj.Tags.Add(bufProjectTag);
                _db.InstructionTag.Add(bufProjectTag);
            }

            AddTargets(project, proj);

            proj.currentSum = 0;
            proj.progress = 0;

            int id = 1;
            if (_db.Project.ToList().Count != 0)
            {
                id = _db.Project.ToList().Last().Id + 1;
            }

            AddVisa(project, id);
            return proj;
        }

        public void AddTargets(CreateProjectInfo project, Project proj)
        {
            foreach (var goals in project.finansalGoals)
            {
                var target = new Target();
                if (_db.Project.ToList().Count != 0)
                {
                    target.projectId = _db.Project.ToList().Last().Id + 1;
                }
                target.cost = goals.cost;
                target.title = goals.title;
                target.IsCompleted = false;
                proj.Targets.Add(target);
                proj.requiredSum += target.cost;
                _db.Step.Update(target);
            }
            _db.Project.Update(proj);
            _db.SaveChanges();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public void UpdateGoals([FromBody] UpdateGoals model)
        {
            var proj = _db.Project.FirstOrDefault(p => p.Id == model.projectId);
            proj.requiredSum = 0;
            var buf = new CreateProjectInfo
            {
                finansalGoals = model.goals
            };
            var steps = _db.Step.ToList().FindAll(p => p.projectId == model.projectId);
            foreach (var step in steps)
            {
                _db.Step.Remove(step);
            }
            _db.SaveChanges();
            AddTargets(buf, proj);
        }

        public void AddVisa(CreateProjectInfo project, int id)
        {
            _db.Visa.Add(FactoryVisa(project, id));
        }

        public Visa FactoryVisa(CreateProjectInfo project, int id)
        {
            var visa = new Visa
            {
                Date = project.payment.expirationDate,
                NumberCard = project.payment.cardNumber,
                OwnerName = project.payment.owner,
                Pin = project.payment.cvCode,
                ProjectId = id,
                UserId = project.userId
            };
            return visa;
        }

        public void RefreshDate()
        {
            List<Project> projects = new List<Project>();
            projects = _db.Project.ToList();
            if (projects == null)
            {
                BadRequest("Null projects");
            }
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
            var followProject = new FollowsUser
            {
                UserId = model.UserId,
                ProjectId = model.ProjectId
            };
            _db.FollowsUser.Add(followProject);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult UnFollowProject([FromBody] FollowInfo model)
        {
            var followProject = _db.FollowsUser.ToList().FirstOrDefault(p => (p.ProjectId == model.ProjectId) && (p.UserId == model.UserId));
            _db.FollowsUser.Remove(followProject);
            _db.SaveChanges();
            return Ok();

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
            var profile = new UserProfileMini();
            var rating = _db.Rating.FirstOrDefault(p => (p.UserId == userId) && (p.ProjectId == id));
            projectController.RefreshDate();
            var project = _db.Project.FirstOrDefault(p => p.Id == id);
            var user = _db.UserProfile.FirstOrDefault(p => (p.Id == project.CreateUserId));
            var checkStatus = _db.FollowsUser.ToList().FirstOrDefault(p => (p.UserId == userId) && (p.ProjectId == id));

            CheckStatusProject(checkStatus, project);

            Checkrating(rating, project);

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

            profile.id = user.Id;
            profile.firstName = user.FirstName;
            profile.secondName = user.SecondName;
            profile.urlPhoto = user.UrlPhoto;

            display.project = project;
            display.tags = tagsString;
            display.finansalGoal = GetGoals(id);
            display.creater = profile;


            return new ObjectResult(display);
        }

        public List<FinansalGoal> GetGoals(int id)
        {
            var goals = new List<FinansalGoal>();
            foreach (var tar in _db.Step.ToList().FindAll(p => p.projectId == id))
            {
                goals.Add(FactoryGoals(tar));
            }
            return goals;
        }

        public FinansalGoal FactoryGoals(Target tar)
        {
            var finansalGoal = new FinansalGoal
            {
                cost = tar.cost,
                isCompleted = tar.IsCompleted,
                title = tar.title,
            };
            return finansalGoal;
        }

        public void CheckStatusProject(FollowsUser checkStatus, Project project)
        {
            if (checkStatus != null)
            {
                project.followed = true;
            }
            else
            {
                project.followed = false;
            }
        }

        public void Checkrating(Models.Rating rating, Project project)
        {
            if (rating == null)
            {
                project.UserRating = 0;
            }
            else
            {
                project.UserRating = rating.Value;
            }
        }

        //[HttpPost("[action]")]
        //[AllowAnonymous]
        //public IActionResult AddRating([FromBody] ViewModels.Rating model)
        //{
        //    var project = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
        //    if (model.Value > 5 && model.Value < 0)
        //    {
        //        return BadRequest("Pashel nahuy pidor, naebat on reshil");
        //    }
        //    var rating = _db.Rating.FirstOrDefault(p => (p.ProjectId == model.ProjectId) && (p.UserId == model.UserPofileId));
        //    if (rating == null)
        //    {
        //        rating = new Models.Rating
        //        {
        //            ProjectId = model.ProjectId,
        //            UserId = model.UserPofileId,
        //            Value = model.Value
        //        };
        //        project.Rating += model.Value;
        //        _db.Rating.Add(rating);
        //    }
        //    else
        //    {
        //        project.Rating -= rating.Value;
        //        project.Rating += model.Value;
        //        rating.Value = model.Value;
        //        _db.Rating.Update(rating);
        //    }
        //    _db.Project.Update(project);
        //    _db.SaveChanges();
        //    return Ok("Ok");
        //}

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult AddRating([FromBody] ViewModels.Rating model)
        {
            var project = _db.Project.FirstOrDefault(p => p.Id == model.ProjectId);
            if (model.Value > 5 && model.Value < 0)
            {
                return BadRequest("Pashel nahuy pidor, naebat on reshil");
            }
            var rating = new Models.Rating
            {
                ProjectId = model.ProjectId,
                UserId = model.UserPofileId,
                Value = model.Value
            };
            var usersCount = _db.Rating.ToList().FindAll(p => p.ProjectId == model.ProjectId).Count + 1;
            project.Rating = (int)(((float)project.Rating + (float)model.Value) / (float)usersCount);
            _db.Rating.Add(rating);
            _db.Project.Update(project);
            _db.SaveChanges();
            return new ObjectResult(project.Rating);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult addCommentInProject([FromBody] ViewModels.Commentary model)
        {
            var project = _db.Project.FirstOrDefault(p => p.Id == model.projectid);
            var commentary = new Models.Commentary();

            commentary.Content = model.content;
            commentary.DateCreated = date.ToString();

            commentary.Project = project;
            commentary.IdProject = project.Id;
            commentary.UserProfile = _db.UserProfile.FirstOrDefault(p => p.Id == model.userid);
            commentary.IdUserProfile = commentary.UserProfile.Id;

            _db.Commentary.Add(commentary);
            project.ProjectComments.Add(commentary);
            UpdateProjectDB(project);

            var mini = new UserProfileMini
            {
                firstName = commentary.UserProfile.FirstName,
                secondName = commentary.UserProfile.SecondName,
                id = commentary.UserProfile.Id,
                urlPhoto = commentary.UserProfile.UrlPhoto
            };

            var displayView = new ViewModels.Commentary
            {
                content = commentary.Content,
                Id = commentary.Id,
                userProfile = mini,
                dataCreated = commentary.DateCreated,
                userid = commentary.UserProfile.Id,
                projectid = commentary.Project.Id
            };

            return new ObjectResult(displayView);
        }

        public void UpdateProjectDB(Project project)
        {
            _db.Project.Update(project);
            _db.SaveChanges();
        }

        [HttpGet("[action]/{projectId}")]
        public IActionResult getCommentsByProjects(int projectId)
        {
            List<Models.Commentary> commentarys = _db.Commentary.ToList().FindAll(p => p.IdProject == projectId);
            var coments = new List<ViewModels.Commentary>();

            foreach (var com in commentarys)
            {
                var comment = new ViewModels.Commentary();
                comment.Id = com.Id;
                comment.projectid = com.IdProject;
                comment.dataCreated = com.DateCreated;
                comment.content = com.Content;
                comment.userid = com.IdUserProfile;

                var userProf = _db.UserProfile.FirstOrDefault(p => p.Id == com.IdUserProfile);

                var mini = new UserProfileMini
                {
                    firstName = userProf.FirstName,
                    secondName = userProf.SecondName,
                    id = userProf.Id,
                    urlPhoto = userProf.UrlPhoto
                };
                comment.userProfile = mini;
                coments.Add(comment);
            }
            return new ObjectResult(coments);
        }

        [HttpPost("[action]")]
        public IActionResult removeCommentInProject([FromBody] ViewModels.RemoveComment model)
        {
            var commentary = _db.Commentary.ToList().FirstOrDefault(p => p.Id == model.id);
            if (commentary.IdUserProfile == model.userid || _db.UserProfile.FirstOrDefault(p => p.Id == model.userid).Role == "Admin")
            {
                _db.Commentary.Remove(commentary);
                _db.SaveChanges();
            }
            else
            {
                BadRequest("Error. 401 Unauthorized.");
            }
            return Ok();
        }

        [HttpGet("getProjects/{take}/{skip}/{property}/{type}/{value}")]
        public List<Models.Project> GetInstructionDefaulttype(int take, int skip, string property, string type, string value)
        {
            var projects = new List<Project>();
            switch (property)
            {
                case "all":
                    projects = _db.Project.ToList();
                    break;
                case "new":
                    projects = _db.Project.ToList();
                    var responseProjects = new List<Project>();
                    foreach (var project in projects.ToList())
                    {
                        elapsed = date.Subtract(Convert.ToDateTime(project.DateCreated));
                        if (elapsed.Days < 2)
                        {
                            responseProjects.Add(project);
                        }
                    }
                    projects = responseProjects;
                    break;
                case "success":
                    projects = _db.Project.ToList().FindAll(p => p.Status == true);
                    break;
            }
            return projects;
        }

        
        [HttpPost("[action]")]
        public IActionResult donateInProject([FromBody] Donate model)
        {
            var project = _db.Project.FirstOrDefault(p => p.Id == model.projectId);
            AddDonate(model, project);
            _db.SaveChanges();
            return Ok();
        }

        public void AddDonate(Donate model, Project project)
        {
            project.currentSum += model.purchase;
            project.progress = (int)(((float)project.currentSum / (float)project.requiredSum) * 100);
            var sponsor = FactorySponsor(model);
            project.Sponsors = CheckSponsor(model, project, sponsor);
            _db.Project.Update(project);
        }

        public int CheckSponsor(Donate model, Project project, Sponsors sponsor)
        {
            if (_db.Sponsors.FirstOrDefault(p => (p.UserId == model.userId) && (p.ProjectId == model.projectId)) == null)
            {
                project.Sponsors++;
                _db.Sponsors.Add(sponsor);
            }
            else
            {
                _db.Sponsors.Update(sponsor);
            }
            return project.Sponsors;
        }

        public Sponsors FactorySponsor(Donate model)
        {
            var sponsor = new Sponsors
            {
                Money = model.purchase,
                ProjectId = model.projectId,
                UserId = model.userId
            };
            return sponsor;
        }

    }
}