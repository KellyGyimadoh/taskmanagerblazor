using System;
using System.Net.Http.Json;
using TaskManager.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace TaskManager.Clients;

public class UserTaskClient(HttpClient httpClient)
{

    private readonly List<UserTask> tasks = [

    ];

    //public UserTask[] GetUserTasks()=>tasks.ToArray();
 public async Task <UserTask[]> GetUserTasksAsync()
 =>await httpClient.GetFromJsonAsync<UserTask[]>("api/tasks")??[];
/*public async Task<(UserTask[] Tasks, int TotalPages)> GetUserTasksAsync(int page, int pageSize)
{
    try
    {
        var response = await httpClient.GetAsync($"api/tasks?_page={page}&_limit={pageSize}");
        
        if (response.IsSuccessStatusCode)
        {
            var taskResponse = await response.Content.ReadFromJsonAsync<PaginatedResponse<UserTask>>();
            
            if (taskResponse != null)
            {
                return (taskResponse.Data.ToArray(), taskResponse.LastPage);
            }
        }
        
        // Handle error cases here
        return (Array.Empty<UserTask>(), 0);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        return (Array.Empty<UserTask>(), 0);
    }
}*/

   public async Task AddUserTaskAsync(UserTaskDetails taskDetails)
{
    var response = await httpClient.PostAsJsonAsync("api/tasks", taskDetails);

    
    if (!response.IsSuccessStatusCode)
    {
        // Handle error response
        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error: {errorContent}");
    }

}

    public async Task<UserTaskDetails> GetTaskDetailsAsync(int id)
    => await httpClient.GetFromJsonAsync<UserTaskDetails>($"api/tasks/{id}") ?? throw new Exception("Task not found");

    public async Task DeleteUserTaskAsync(int id) => await httpClient.DeleteAsync($"api/tasks/{id}");

    public async Task DeleteAllTasksAsync()=>await httpClient.DeleteAsync("api/tasks");

    public async Task UpdateUserTaskAsync(UserTaskDetails updatedUserTask)
    => await httpClient.PutAsJsonAsync($"api/tasks/{updatedUserTask.Id}", updatedUserTask);

    /*public async Task UpdateTaskStatusAsync(UserTask task)
    {
        var url = $"tasks/{task.Id}";
        var updatedTask = new { task.Status }; // Sending only the updated part

        var response = await httpClient.PutAsJsonAsync(url, updatedTask);
        response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
    } */
   public async Task UpdateTaskStatusAsync(UserTask updatedUserTask)
{
    var url = $"api/status/tasks/{updatedUserTask.Id}";
    var updatedStatus = new { status = updatedUserTask.Status };

    var jsonContent = JsonSerializer.Serialize(updatedStatus);
    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    var response = await httpClient.PatchAsync(url, content);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Status updated successfully");
    }
    else
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error updating status: {errorContent}");
    }
}

    // private async Task LoadPage(int page)
    // {
    //     const int pageSize = 10;
    //     var response = await HttpClient.GetAsync($"tasks?_page={page}&_limit={pageSize}");
    //     var totalCount = int.Parse(response.Headers.GetValues("X-Total-Count").FirstOrDefault() ?? "0");
    //     totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    //     currentPage = page;
    //     tasks = await response.Content.ReadFromJsonAsync<UserTask[]>();
    // }

    // public async Task<IEnumerable<UserTask>> SearchTasksAsync(string searchTerm)
    // {

    //     var response = await httpClient.GetAsync($"tasks?query={Uri.EscapeDataString(searchTerm)}");
    //     response.EnsureSuccessStatusCode();
    //     return await response.Content.ReadFromJsonAsync<IEnumerable<UserTask>>() ??[];
    // }


}
