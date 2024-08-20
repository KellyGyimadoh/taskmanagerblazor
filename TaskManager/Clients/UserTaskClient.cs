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



public async Task<IEnumerable<UserTask>> SearchTasksAsync(string searchTerm)
{
    // Example API call to search tasks
    var response = await httpClient.GetAsync($"tasks?query={Uri.EscapeDataString(searchTerm)}");
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync<IEnumerable<UserTask>>() ??[];
}

//private readonly Priority[] priorities= new PriorityClient().GetPriority();

/*public void  AddUserTask(UserTaskDetails taskDetails){
    var priority= GetPriorityEnum(taskDetails.PriorityId);
    var userTask= new UserTask{
        Id= taskDetails.Id+1,
        Name= taskDetails.Name,
        Priority= priority.Name.ToString(),
        Description=taskDetails.Description,
        DeadlineDate= taskDetails.DeadlineDate,
        Status= taskDetails.Status

    };
    tasks.Add(userTask);
} */

/*public UserTaskDetails GetTaskDetails(int id){
    UserTask usertask= GetUserTaskId(id);
     var priority = priorities.Single(priority => string.Equals(priority.Name.ToString(),
                usertask.Priority,
                StringComparison.OrdinalIgnoreCase));

   return new UserTaskDetails{
    Id=usertask.Id,
    Name=usertask.Name,
    PriorityId=priority.Id.ToString(),
    DeadlineDate= usertask.DeadlineDate,
    Description= usertask.Description,
    Status= usertask.Status
   };
} */


/*public void DeleteUserTask(int id){
    var usertask= GetUserTaskId(id);
    tasks.Remove(usertask);
}*/


//update

/*public void UpdateUserTask(UserTaskDetails updatedUserTask){

    var priority= GetPriorityEnum(updatedUserTask.PriorityId);
    UserTask existingTask= GetUserTaskId(updatedUserTask.Id);
    existingTask.Name= updatedUserTask.Name;
    existingTask.Description= updatedUserTask.Description;
    existingTask.Priority= priority.Name.ToString();
    existingTask.DeadlineDate= updatedUserTask.DeadlineDate;
    existingTask.Status= updatedUserTask.Status;

    
}*/


/*public void UpdateTaskStatus(UserTask task)
{
    var existingtask = GetUserTaskId(task.Id);
    if (existingtask != null)
    {
        existingtask.Status = task.Status;
        // Add any logic here to save changes, e.g., saving to a database or updating an API.
    }
}*/



/*private Priority GetPriorityEnum(string? id){
    ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

    return priorities.Single(priority=>priority.Id==int.Parse(id));
}

private UserTask GetUserTaskId(int id){
    UserTask? task = tasks.Find(task=>task.Id==id);
    ArgumentNullException.ThrowIfNull(task);
    return task;
}*/
}
