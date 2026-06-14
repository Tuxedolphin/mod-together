using System.Text.Json.Serialization;

namespace Backend.Models;

// Note that this is used in DTOs as well, so any sensitive data should be hidden
public class Profile
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string? Username { get; set; }
}
