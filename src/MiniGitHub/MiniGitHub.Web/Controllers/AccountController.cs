using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services;
using MiniGitHub.Web.Extensions;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class AccountController(
    IUserService userService,
    IAuthService authService
) : Controller 
{
    [HttpGet]
    public IActionResult Index() {
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    public IActionResult Login() {
        if (User.Identity?.IsAuthenticated ?? false) {
            return RedirectToAction("Index", "Home");
        } 
        
        UserLoginDTO dto = new();
        return View(dto);
    }

    private async Task SetCookiesForUser(User user) {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    } 
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginDTO dto) {
        try {
            User? user = userService.GetUserByUsername(dto.UsernameOrEmail) ?? userService.GetUserByEmail(dto.UsernameOrEmail);
            
            if (user is null) {
                ModelState.AddModelError("UsernameOrEmail", "User with this username or email doesn't exist.");
                return View("Login", dto);
            }

            if (!authService.ValidateLogin(user.Username, dto.Password)) { 
                ModelState.AddModelError("Password", "You entered a wrong password.");
                return View("Login", dto);
            }

            await SetCookiesForUser(user);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception e) {
            ModelState.AddModelError("", e.Message);
            return View("Login", dto);
        }
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult Register() {
        if (User.Identity?.IsAuthenticated ?? false) {
            return RedirectToAction("Index", "Home");
        } 
        
        UserRegisterDTO dto = new UserRegisterDTO();
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterDTO dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }
        
        try {
            if (userService.UserWithUsernameExists(dto.Username)) {
                ModelState.AddModelError("Username", "User with this username already exists.");
                return View(dto);
            }

            if (userService.UserWithEmailExists(dto.Email)) {
                ModelState.AddModelError("Email", "User with this email already exists.");
                return View(dto);
            }
            
            User user = new User(-1, dto.Username, dto.Email, dto.Password);
            user = userService.AddUser(user);
            
            await SetCookiesForUser(user);
            
            return RedirectToAction("Detail", "User", new {id = user.Id});
        }
        catch (Exception e) {
            ModelState.AddModelError("Register", e.Message);
            return View(dto);
        }
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult OnlyAuthorized() {
        return View();
    }
    
    
    [HttpGet]
    [Authorize]
    public IActionResult Profile() {
        long? userId = User.TryGetUserId();
        if (userId is null) {
            return RedirectToAction("Login");
        }
        
        return RedirectToAction("Detail", "User", new {id = User.TryGetUserId()});
    }
}