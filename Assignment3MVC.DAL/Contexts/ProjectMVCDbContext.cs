using Assignment3MVC.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3MVC.DAL.Contexts
{
    public class ProjectMVCDbContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectMVCDbContext(DbContextOptions<ProjectMVCDbContext> options):base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Server = .; Database = ProjectMVCDb; Trusted_Connection = true;");

            public DbSet<Department> Departments { get; set; }
            public DbSet<Employee> Employees { get; set; }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
