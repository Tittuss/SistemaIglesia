using Client;
using Client.Services;
using Client.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentViewModel, StudentViewModel>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITeacherViewModel, TeacherViewModel>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseViewModel, CourseViewModel>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5144") });

await builder.Build().RunAsync();
