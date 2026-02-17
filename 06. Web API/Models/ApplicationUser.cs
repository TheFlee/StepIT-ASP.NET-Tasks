using Microsoft.AspNetCore.Identity;

namespace _06._Web_API.Models;

public class ApplicationUser: IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; } = null!;

    public IEnumerable<ProjectMember> ProjectMemberships { get; set; }
        = new List<ProjectMember>();
}
