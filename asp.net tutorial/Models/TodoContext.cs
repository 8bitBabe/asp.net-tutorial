using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asp.net_tutorial.Models
{
    public class TodoContext : DbContext //services like the db must be registered with the dependency injection container.
        //It provides the service to controllers
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
