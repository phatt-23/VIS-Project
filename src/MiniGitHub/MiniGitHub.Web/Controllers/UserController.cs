using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Repositories;

namespace MiniGitHub.Web.Controllers;

public class UserController : Controller{
    public UserController(UserRepository userRepository) {
        _userRepository = userRepository;
    }
        
    [HttpGet]
    public IActionResult Index() {
        ViewData["Users"] = _userRepository.GetAllUsers();
        return View();
    }

    [HttpGet]
    public IActionResult Details(int id) {
        User? user = _userRepository.GetUserWithRepositories(id);
        if (user == null) {
            return NotFound();
        }

        ViewData["User"] = user;
        return View();
    }

    private readonly UserRepository _userRepository;
}