using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        //Dependency Injection
        public DataContext(DbContextOptions options) : base(options) 
        {
        }

        //DbSet : Takes The Type Of The Class We Want To Create A Database Set For It
        //Users : The Table Name
        public DbSet<AppUser> Users { get; set; }

        //Next step => add this configuration to our Start-Up class so that we can inject the data context
        //into other parts of our application. 
        //(press ctrl + p and search startup class) and (go to ConfigureServices method)
    }
}