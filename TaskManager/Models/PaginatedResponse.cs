using System;
using System.Text.Json.Serialization;

namespace TaskManager.Models;

  public class PaginatedResponse<T>
{
    public List<T> Data { get; set; } = new List<T>();
    public int Total { get; set; }
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }
    [JsonPropertyName("last_page")]
    public int LastPage { get; set; }
}
