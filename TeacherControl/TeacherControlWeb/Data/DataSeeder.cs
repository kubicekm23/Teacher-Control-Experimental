using Microsoft.AspNetCore.Identity;
using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
    {
        // Ensure roles exist
        foreach (var role in new[] { "Admin", "User" })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Seed initial admin user if none exists
        if (!userManager.Users.Any())
        {
            var admin = new UserEntity
            {
                UserName = "admin",
                Email = "admin@template.com",
                EmailConfirmed = true,
            };

            // You should change this password on first login
            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Seed some teachers
        if (!dbContext.Teachers.Any())
        {
            dbContext.Teachers.AddRange(new List<TeacherEntity>
            {
                new TeacherEntity { Id = Guid.NewGuid(), FirstName = "Jan", LastName = "Novák", Subject = "Matematika", Description = "Legenda školy." },
                new TeacherEntity { Id = Guid.NewGuid(), FirstName = "Marie", LastName = "Svobodová", Subject = "Český jazyk", Description = "Velmi přísná ale spravedlivá." },
                new TeacherEntity { Id = Guid.NewGuid(), FirstName = "Petr", LastName = "Dvořák", Subject = "Tělesná výchova", Description = "Vždy v dobré náladě." }
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
