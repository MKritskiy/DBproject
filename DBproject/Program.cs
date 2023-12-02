
using DBproject.BL.Auth;
using DBproject.BL.TaskList;
using DBproject.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<DBproject.BL.Team.ITeam, DBproject.BL.Team.Team>();
builder.Services.AddScoped<ITaskList, TaskList>();

builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddSingleton<IExecutorDAL, ExecutorDAL>();
builder.Services.AddSingleton<IRoleDAL, RoleDAL>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ITeamDAL, TeamDAL>();
builder.Services.AddSingleton<ITaskListDAL, TaskListDAL>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
    });
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
