using System;  
using System.Collections.Generic;   
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project_HU.Models;

namespace Project_HU;
public class TaskContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Label> Labels { get; set; }


        public TaskContext(DbContextOptions options):base(options)
        {
            
        }

    }