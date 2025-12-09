using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class HomeController(
    IUserService userService, 
    IRepositoryService repoService
) : Controller {

    public IActionResult Index() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult Search(string query = "", SearchFilter filter = SearchFilter.All) {
        SearchDTO dto = new SearchDTO() {
            Query = query,
            Filter = filter,
            Users = new List<User>(),
            Repos = new List<Repository>(),
        };

        if (filter is SearchFilter.All or SearchFilter.Users) {
            List<User> users = userService.GetAllUsers()
                .Where(u => u.Username.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
            
            dto.Users = users;
        }

        if (filter is SearchFilter.All or SearchFilter.Repos) {
            List<Repository> repos = repoService.GetAllRepos()
                .Where(r => r.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (Repository repo in repos) {
                User user = userService.GetUserById(repo.OwnerId)!;
                repo.Owner = user;
            }
            
            dto.Repos = repos;
        }
        
        return View(dto);
    }
}