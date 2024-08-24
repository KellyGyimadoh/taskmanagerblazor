using System.Net.Http.Headers;
using System.Security.Cryptography;
using Syncfusion.Blazor;
using TaskManager.Clients;
using TaskManager.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// builder.Services.AddSingleton<UserTaskClient>();
// builder.Services.AddSingleton<PriorityClient>();

var taskAppUrl= builder.Configuration["appUrl"] ?? throw new Exception("Failed to load backend file");
builder.Services.AddHttpClient<UserTaskClient>(Client=>{Client.BaseAddress=new Uri(taskAppUrl);
Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

});
builder.Services.AddHttpClient<PriorityClient>(Client=>Client.BaseAddress=new Uri(taskAppUrl));
builder.Services.AddSyncfusionBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
