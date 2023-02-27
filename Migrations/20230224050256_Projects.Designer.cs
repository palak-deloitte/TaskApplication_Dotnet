﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_HU;

#nullable disable

namespace Project_HU.Migrations
{
    [DbContext(typeof(TaskContext))]
    [Migration("20230224050256_Projects")]
    partial class Projects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Project_HU.Models.Project", b =>
                {
                    b.Property<int>("project_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("project_id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Project_HU.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_HU.Models.UserRole", b =>
                {
                    b.Property<int>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("role_id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.Property<int>("Creatoruser_id")
                        .HasColumnType("int");

                    b.Property<int>("Projectsproject_id")
                        .HasColumnType("int");

                    b.HasKey("Creatoruser_id", "Projectsproject_id");

                    b.HasIndex("Projectsproject_id");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.Property<int>("UserRolesrole_id")
                        .HasColumnType("int");

                    b.Property<int>("Usersuser_id")
                        .HasColumnType("int");

                    b.HasKey("UserRolesrole_id", "Usersuser_id");

                    b.HasIndex("Usersuser_id");

                    b.ToTable("UserUserRole");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.HasOne("Project_HU.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Creatoruser_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_HU.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("Projectsproject_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.HasOne("Project_HU.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("UserRolesrole_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_HU.Models.User", null)
                        .WithMany()
                        .HasForeignKey("Usersuser_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
