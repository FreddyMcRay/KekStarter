using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.Models
{
    public class Target
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Project Project { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<Block> Blocks { get; set; }

        public Target()
        {
            Blocks = new List<Block>();
        }
    }

    public class Block
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonIgnore]
        [Required]
        public Target Target { get; set; }
        public string Type { get; set; }
        public string Field { get; set; }
    }

    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string DateCreated { get; set; }
        public string DateEnd { get; set; }
        public string urlImage { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int requiredSum { get; set; }
        public int currentSum { get; set; }
        public int Sponsors { get; set; }
        public bool Status { get; set; }
        public int CreateUserId { get; set; }
        public int leftOver { get; set; }
        public int progress { get; set; }
        public string image { get; set; }

        [NotMapped]
        public ICollection<UserProfile> UserProfiles { get; set; }

        public ICollection<ProjectTag> Tags { get; set; }
        public ICollection<Target> Targets { get; set; }
        public ICollection<ProjectNew> ProjectNews { get; set; }
        public ICollection<Commentary> ProjectComments { get; set; }

        public Project()
        {
            Targets = new List<Target>();
            Tags = new List<ProjectTag>();
            ProjectNews = new List<ProjectNew>();
            ProjectComments = new List<Commentary>();
            UserProfiles = new List<UserProfile>();
        }

    }

    public class Commentary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Project Project { get; set; }
        public UserProfile UserProfile { get; set; }
        public string DateCreated { get; set; }
        public string Content { get; set; }

    }

    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Project> Projects { get; set; }

        public Tag()
        {
            Projects = new List<Project>();
        }
    }

    public class ProjectTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Tag Tag { get; set; }
        [Required]
        [JsonIgnore]
        public Project Project { get; set; }
    }

    public class Achivment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; }
    }

    public class AchivmentUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Achivment Achivment { get; set; }
        [JsonIgnore]
        public UserProfile UserProfile { get; set; }
    }

    public class ProjectNew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Project Project { get; set; }
        public string DataCreated { get; set; }
        public string Description { get; set; }
    }
}
