namespace Backend.Models;

// Note that this is used in DTOs as well, so any sensitive data should be hidden
public class Profile
{
    public Guid Id { get; set; }
    public string? Username { get; set; }

    // We only store this as the path of avatar is deterministic
    // i.e. it is always {UserId}/avatar.webp
    public DateTime? AvatarUpdatedAt { get; set; }
}
