using Microsoft.EntityFrameworkCore;
using ShareYourSword.Models;
using System.Collections.Generic;

namespace ShareYourSword.Assets
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
