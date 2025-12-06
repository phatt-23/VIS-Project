using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;
using MiniGitHub.Domain.Services;
using MiniGitHub.Domain.TransactionScript;
using MiniGitHub.Web.Extensions;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class UserController(
    IUserService userService, 
    UserMapper userMapper,
    RepositoryMapper repositoryMapper,
    GetUserWithRepositoriesTS getUserWithRepositoriesTs
) : Controller
{

    [HttpGet]
    public IActionResult Index() {
        ViewData["Users"] = userService.GetAllUsers();
        return View();
    }

    public IActionResult Detail(long id) {
        User? user = getUserWithRepositoriesTs.Run(id);
        
        if (user == null) {
            return NotFound("User not found");
        }

        return View(user);
    }

    [HttpGet]
    public IActionResult Add() {
        return View(new AddUserDTO());
    }
    
    [HttpPost]
    public IActionResult Add(AddUserDTO dto) {
        User user = new User(-1, dto.Username, dto.Email, dto.Password);

        user = userService.AddUser(user);
        
        return Redirect("Index");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Edit() {
        EditUserDTO dto = new EditUserDTO() {
            Username = User.GetUsername(),
            Email = User.GetEmail(),
        };
        return View(dto);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Edit(EditUserDTO dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }

        if (userService.UserWithUsernameExists(dto.Username)) {
            ModelState.AddModelError("Username", "User with this username already exists.");
            return View(dto);
        }

        if (userService.UserWithEmailExists(dto.Email)) {
            ModelState.AddModelError("Email", "User with this email already exists.");
            return View(dto);
        }

        User? user = userService.GetUserByUsername(User.GetUsername());
        if (user == null) {
            return Forbid("User must exist to edit profile.");
        }
        
        user.Username = dto.Username;
        user.Email = dto.Email;

        if (userService.UpdateUser(user) is null) {
            return Forbid("Unable to update user.");
        }
        
        return RedirectToAction("Detail", new {id = User.TryGetUserId()}); 
    }
}