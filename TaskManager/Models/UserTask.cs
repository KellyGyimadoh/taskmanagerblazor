using System;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace TaskManager.Models;

public class UserTask
{
    public int Id { get; set; }

 [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

 [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("deadline_date")]
    public DateOnly? DeadlineDate { get; set; }

    [JsonPropertyName("priority_id")]
    public int PriorityId { get; set; }

    [JsonIgnore]
    public LevelOfPriority Priority => (LevelOfPriority)PriorityId;

    [JsonConverter(typeof(BooleanIntConverter))]
    public bool Status { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public enum TheTaskStatus
{
    Pending = 1,
    Completed = 2
}

public enum LevelOfPriority
{
    Low = 1,
    Normal = 2,
    High = 3,
    Critical = 4
}