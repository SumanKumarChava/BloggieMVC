using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Seed roles - User,Admin, SuperAdmin
        var superAdminRoleId = "1e819d66-1ae7-41d3-9903-cbcc976dba9e";
        var adminRoleId = "2930349c-8e13-4d52-bd6b-09cbd66235dd";
        var userRoleId = "2a7659d0-2f83-4e2a-8358-5b4fecd939d5";
        
        var roles = new List<IdentityRole>()
        {
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole()
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            },
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
        
        // Seed SuperAdmin User
        var superAdminUserId = "61cb8d7d-4bce-42b7-afae-a30b7540a38e";
        var superAdminUser = new IdentityUser()
        {
            UserName = "superadmin@bloggie.com",
            Email = "superadmin@bloggie.com",
            NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
            NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
            Id = superAdminUserId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");
        builder.Entity<IdentityUser>().HasData(superAdminUser);

        // Add All roles to SuperAdmin user
        var superAdminRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminUserId
            },
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminUserId
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = superAdminUserId
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}