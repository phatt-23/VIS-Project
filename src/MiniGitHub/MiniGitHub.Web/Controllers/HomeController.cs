using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Repositories;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class HomeController(
    ILogger<HomeController> logger, 
    IRepositoryRepository repositoryRepository   
) : Controller {

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
}