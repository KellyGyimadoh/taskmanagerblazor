using System;

namespace TaskManager.Models;
 public enum TheTaskStatus{
        Pending=1,
        Completed=2
    }

    public enum LevelOfPriority{
       Low = 1,
    Normal = 2,
    High = 3,
    Critical = 4
    }
public class UserTask()
{
public  int Id{get; set;}

public required string Name{get; set;}= string.Empty;

public string? Description {get; set;}

public required int PriorityId{get; set;}

public LevelOfPriority Priority => (LevelOfPriority)PriorityId;
public bool Status{get; set;}=false;


public DateOnly? DeadlineDate{get; set;}= DateOnly.FromDateTime(DateTime.Now);

// public UserTask(){
//      Name = string.Empty; // Default value
//         Priority = "Normal"; // Default value
// }

}
