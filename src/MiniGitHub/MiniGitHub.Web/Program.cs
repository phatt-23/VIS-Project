using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DAOs.SqlDAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Domain.Mappers;
using MiniGitHub.Domain.Repositories;
using MiniGitHub.Domain.Services.TransactionScriptPattern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); 

builder.Services.AddScoped<IDataConnector, SqlDataConnector>();

// DAOs should not be present in here, they should only appear in the domain layer
// builder.Services.AddScoped<IUserDao, UserSqlDao>();
// builder.Services.AddScoped<IRepositoryDao, RepositorySqlDao>();

// repositories
builder.Services.AddScoped<IRepositoryRepository, RepositoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommitRepository, CommitRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();


// transaction scripts
builder.Services.AddScoped<GetUserWithRepositoriesTS>();

// mappers
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<RepositoryMapper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) { // Configure the HTTP request pipeline.
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); 
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();