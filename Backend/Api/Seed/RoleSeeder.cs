using Microsoft.AspNetCore.Identity;

namespace Api.Seed;

public static class RoleSeeder
{
    private static readonly string[] Roles = [
        "SuperAdmin",
        "ClinicAdmin",
        "Doctor",
        "Nurse",
        "Receptionist",
        "Accountant",
        "Patient"
        ];

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<
            RoleManager<IdentityRole<Guid>>>();

        foreach (var role in Roles)
        {
            if (await roleManager.RoleExistsAsync(role))
            {
                continue;
            }

            var result = await roleManager.CreateAsync(
                new IdentityRole<Guid>(role));

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(error => error.Description));

                throw new InvalidOperationException(
                    $"Failed to create role '{role}': {errors}");
            }
        }
    }
}
