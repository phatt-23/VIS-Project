using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;
using MiniGitHub.Domain.Repositories;
using MiniGitHub.Domain.Services.TransactionScriptPattern;

namespace MiniGitHub.Web.Controllers;

public class UserController(
    IUserRepository userRepository, 
    UserMapper userMapper,
    RepositoryMapper repositoryMapper,
    GetUserWithRepositoriesTS getUserWithRepositoriesTs
) : Controller
{
    [HttpGet]
    public IActionResult Index() {
        ViewData["Users"] = userRepository.GetAllUsers();
        return View();
    }

    public IActionResult Detail(long id) {
        // User? user = userRepository.GetUserWithRepositories(id);
        
        
        User? user = getUserWithRepositoriesTs.Run(id);
        
        if (user == null) {
            return NotFound("User not found");
        }

        ViewData["User"] = user;
        return View();
    }
}