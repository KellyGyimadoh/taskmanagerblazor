using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UserTaskDetails
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [JsonConverter(typeof(BooleanIntConverter))]
    [Required]
    public bool Status { get; set; }

    public string? Description { get; set; }

    [Required]
    public DateOnly? Deadline_date { get; set; }

    [Required(ErrorMessage = "Please select a priority level")]
    public int? Priority_id { get; set; }  // Ensure this matches 'priority_id' in your request and database
}
