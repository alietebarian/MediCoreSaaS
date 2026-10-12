using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
}
