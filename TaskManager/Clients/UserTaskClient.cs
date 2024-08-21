using System;
using System.Net.Http.Json;
using TaskManager.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace TaskManager.Clients;

public class UserTaskClient(HttpClient httpClient)
{
   
private readonly List <UserTask> tasks= [
  
];

//public UserTask[] GetUserTasks()=>tasks.ToArray();
public async Task <UserTask[]> GetUserTasksAsync()=>await httpClient.GetFromJsonAsync<UserTask[]>("tasks")??[];

public async Task AddUserTaskAsync(UserTaskDetails taskDetails)
=>await httpClient.PostAsJsonAsync("tasks",taskDetails);

public async Task <UserTaskDetails> GetTaskDetailsAsync(int id)
=>await httpClient.GetFromJsonAsync<UserTaskDetails>($"tasks/{id}") ?? throw new Exception("Task not found");

public async Task DeleteUserTaskAsync(int id)=> await httpClient.DeleteAsync($"tasks/{id}");

public async Task  UpdateUserTaskAsync(UserTaskDetails updatedUserTask)
=>await httpClient.PutAsJsonAsync($"tasks/{updatedUserTask.Id}",updatedUserTask);

/*public async Task UpdateTaskStatusAsync(UserTask task)
{
    var url = $"tasks/{task.Id}";
    var updatedTask = new { task.Status }; // Sending only the updated part

    var response = await httpClient.PutAsJsonAsync(url, updatedTask);
    response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
} */
public async Task UpdateTaskStatusAsync(UserTask updatedUserTask)
{
    var url = $"tasks/{updatedUserTask.Id}";
    var updatedStatus = new { status = updatedUserTask.Status };

    var jsonContent = JsonSerializer.Serialize(updatedStatus);
    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    var response = await httpClient.PatchAsync(url, content);
    response.EnsureSuccessStatusCode();
}



// public async Task<IEnumerable<UserTask>> SearchTasksAsync(string searchTerm)
// {
    
//     var response = await httpClient.GetAsync($"tasks?query={Uri.EscapeDataString(searchTerm)}");
//     response.EnsureSuccessStatusCode();
//     return await response.Content.ReadFromJsonAsync<IEnumerable<UserTask>>() ??[];
// }


}
