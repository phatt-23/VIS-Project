using Microsoft.AspNetCore.Authentication.Cookies;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.DataConnector.SqlConnector;
using MiniGitHub.Domain.Mappers;
using MiniGitHub.Domain.Services;
using MiniGitHub.Domain.TransactionScript;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddSingleton<IDataConnector, SqlDataConnector>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommitService, CommitService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<GetUserWithRepositoriesTS>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<RepositoryMapper>();
builder.Services.AddScoped<CommitMapper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) { 
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); 
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();

app.Run();
