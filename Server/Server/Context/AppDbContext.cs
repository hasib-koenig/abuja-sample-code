using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
        {  
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
               .HasKey(e => new {e.UserId,e.RoleId});
                
            modelBuilder.Entity<UserRole>()
                .HasOne(my => my.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(my => my.UserId);

            modelBuilder.Entity<UserRole>()
               .HasOne(my => my.Role)
               .WithMany(u => u.UserRoles)
               .HasForeignKey(my => my.RoleId);

            

        }
    }
}
