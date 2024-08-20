using System;
using TaskManager.Models;
namespace TaskManager.Clients;

public class PriorityClient(HttpClient httpClient)
{

    public async Task<Priority[]>GetPrioritiesAsync()
    =>await httpClient.GetFromJsonAsync<Priority[]>("priorities")??[];
/*private readonly Priority[] priorities={
    new(){
        Id=1,
        Name= LevelOfPriority.Low
    },
    new(){
        Id=2,
        Name= LevelOfPriority.Normal
    },
    new(){
        Id=3,
        Name= LevelOfPriority.High
    },
    new(){
        Id=4,
        Name= LevelOfPriority.Critical
    },
};

public Priority[] GetPriority()=>priorities.ToArray();
*/

}
