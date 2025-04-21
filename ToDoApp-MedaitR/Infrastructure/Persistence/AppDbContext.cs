using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
