using CourseWork.Data;
using CourseWork.Interfaces;
using CourseWork.Repos;
using CourseWork.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var ConnectionString = builder.Configuration["DBConnectionString"];
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 28))));

//DI
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITeamRepo, TeamRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
builder.Services.AddScoped<IRepositoryRepo, RepositoryRepo>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddScoped<ITaskService, TaskService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();