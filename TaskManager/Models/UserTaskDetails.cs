using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using StringConverter= TaskManager.Converter.StringConverter;

namespace TaskManager.Models;

public class UserTaskDetails
{

public int Id{get; set;}

[Required]
public required string Name{get; set;}
[Required]
public bool Status{get;  set;}

public string? Description{get;set;}

[Required]
public DateOnly? DeadlineDate{get; set;}

[Required(ErrorMessage ="Please select a priority level")]

//[JsonConverter(typeof(StringConverter))]
public  int? PriorityId {get; set;}

}
