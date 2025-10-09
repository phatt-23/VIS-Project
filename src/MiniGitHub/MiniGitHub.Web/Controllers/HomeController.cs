using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Repositories;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class HomeController : Controller {
    public HomeController(ILogger<HomeController> logger, RepositoryRepository repositoryRepository) {
        _logger = logger;
        _repositoryRepository = repositoryRepository;
    }

    public IActionResult Index() {
        ViewData["Message"] = "Welcome to MiniGitub!";
        return View();
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    private readonly ILogger<HomeController> _logger;
    private readonly RepositoryRepository _repositoryRepository;
}