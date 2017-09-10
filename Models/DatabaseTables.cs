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

    public class UserLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public UserProfile UserProfile { get; set; }
        [Required]
        [JsonIgnore]
        public Project Project { get; set; }
    }

    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string DateEnd { get; set; }
        public string PreviewImageUrl { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int SumRequired { get; set; }
        public int SumCurrent { get; set; }
        public bool Status { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<ProjectTag> Tags { get; set; }
        public ICollection<Target> Targets { get; set; }
        public ICollection<UserLike> UsersLike { get; set; }
        public ICollection<ProjectNew> ProjectNews { get; set; }
        public ICollection<Commentary> ProjectComments { get; set; }

        public Project()
        {
            Targets = new List<Target>();
            Tags = new List<ProjectTag>();
            UsersLike = new List<UserLike>();
            ProjectNews = new List<ProjectNew>();
            ProjectComments = new List<Commentary>();
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
        public ICollection<ProjectTag> Projects { get; set; }

        public Tag()
        {
            Projects = new List<ProjectTag>();
        }
    }

    public class ProjectTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ProjectTag Tag { get; set; }
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
