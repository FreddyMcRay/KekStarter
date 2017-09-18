using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public static DbContextOptions<ApplicationContext> _options;
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Target> Step { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<ProjectTag> InstructionTag { get; set; }
        public DbSet<Commentary> Commentary { get; set; }
        public DbSet<Achivment> Achivment { get; set; }
        public DbSet<AchivmentUser> AchivmentUser { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<ProjectNew> ProjectNew { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            _options = options;
        }


        public ApplicationContext()
        {

        }
    }
}
