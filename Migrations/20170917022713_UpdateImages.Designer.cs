﻿// <auto-generated />
using KekStarter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace KekStarter.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20170917022713_UpdateImages")]
    partial class UpdateImages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KekStarter.Models.Achivment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UrlImage");

                    b.HasKey("Id");

                    b.ToTable("Achivment");
                });

            modelBuilder.Entity("KekStarter.Models.AchivmentUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AchivmentId");

                    b.Property<int?>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("AchivmentId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("AchivmentUser");
                });

            modelBuilder.Entity("KekStarter.Models.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Field");

                    b.Property<int>("TargetId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("TargetId");

                    b.ToTable("Block");
                });

            modelBuilder.Entity("KekStarter.Models.Commentary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("DateCreated");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Commentary");
                });

            modelBuilder.Entity("KekStarter.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreateUserId");

                    b.Property<string>("DateCreated");

                    b.Property<string>("DateEnd");

                    b.Property<string>("Description");

                    b.Property<int>("Rating");

                    b.Property<int>("Sponsors");

                    b.Property<bool>("Status");

                    b.Property<string>("Title");

                    b.Property<int?>("UserProfileId");

                    b.Property<int>("currentSum");

                    b.Property<string>("image");

                    b.Property<int>("leftOver");

                    b.Property<int>("progress");

                    b.Property<int>("requiredSum");

                    b.Property<string>("urlImage");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("KekStarter.Models.ProjectNew", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DataCreated");

                    b.Property<string>("Description");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectNew");
                });

            modelBuilder.Entity("KekStarter.Models.ProjectTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProjectId");

                    b.Property<int?>("TagId");

                    b.Property<int?>("TagId1");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TagId");

                    b.HasIndex("TagId1");

                    b.ToTable("InstructionTag");
                });

            modelBuilder.Entity("KekStarter.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("KekStarter.Models.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsCompleted");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.Property<string>("Price");

                    b.Property<int>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Step");
                });

            modelBuilder.Entity("KekStarter.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("KekStarter.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("FirstName");

                    b.Property<string>("Language");

                    b.Property<string>("LastLogInDate");

                    b.Property<string>("RegistrationDate");

                    b.Property<string>("Role");

                    b.Property<string>("SecondName");

                    b.Property<string>("UrlPhoto");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KekStarter.Models.AchivmentUser", b =>
                {
                    b.HasOne("KekStarter.Models.Achivment", "Achivment")
                        .WithMany()
                        .HasForeignKey("AchivmentId");

                    b.HasOne("KekStarter.Models.UserProfile", "UserProfile")
                        .WithMany("Achivments")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("KekStarter.Models.Block", b =>
                {
                    b.HasOne("KekStarter.Models.Target", "Target")
                        .WithMany("Blocks")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KekStarter.Models.Commentary", b =>
                {
                    b.HasOne("KekStarter.Models.Project", "Project")
                        .WithMany("ProjectComments")
                        .HasForeignKey("ProjectId");

                    b.HasOne("KekStarter.Models.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("KekStarter.Models.Project", b =>
                {
                    b.HasOne("KekStarter.Models.UserProfile")
                        .WithMany("Projects")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("KekStarter.Models.ProjectNew", b =>
                {
                    b.HasOne("KekStarter.Models.Project", "Project")
                        .WithMany("ProjectNews")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("KekStarter.Models.ProjectTag", b =>
                {
                    b.HasOne("KekStarter.Models.Project", "Project")
                        .WithMany("Tags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KekStarter.Models.ProjectTag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.HasOne("KekStarter.Models.Tag")
                        .WithMany("Projects")
                        .HasForeignKey("TagId1");
                });

            modelBuilder.Entity("KekStarter.Models.Target", b =>
                {
                    b.HasOne("KekStarter.Models.Project", "Project")
                        .WithMany("Targets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KekStarter.Models.UserProfile", b =>
                {
                    b.HasOne("KekStarter.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KekStarter.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KekStarter.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KekStarter.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KekStarter.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
