using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services;
using MiniGitHub.Web.Extensions;
using MiniGitHub.Web.Models;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Web.Controllers;

public class RepositoryController(
    IRepositoryService repoService,
    IFileService fileService,
    ICommitService commitService,
    IUserService userService,
    IIssueService issueService,
    ICommentService commentService
) : Controller {
    
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult Detail(long id) {
        Repository? repo = repoService.GetRepo(id);
        
        if (repo == null) {
            return NotFound("Repository not found");
        }
        
        if (!repo.IsPublic && repo.OwnerId != User.TryGetUserId()) {
            return Unauthorized("Repository is private");
        }

        List<File> latestFiles = fileService.GetLatestCommittedFiles(repo.RepositoryId);
        List<Commit> commits = commitService.GetCommitsForRepo(repo.RepositoryId);
        List<Issue> issues = issueService.GetIssuesForRepo(repo.RepositoryId);
        
        issues.ForEach(i => {
            List<Comment> comments = commentService.GetCommentsForIssue(i.IssueId);
            User creator = userService.GetUserById(i.CreatorId)!;
            i.Comments = comments;
            i.Creator = creator;
        });
        
        User? owner = userService.GetUserById(repo.OwnerId);
        if (owner == null) {
            return NotFound("Owner not found");
        }
            
        repo.Owner = owner;
        
        RepoDetailViewModel model = new RepoDetailViewModel() {
            Repo = repo,
            Owner = repo.Owner,
            LatestFiles = latestFiles,
            Commits = commits,
            Issues = issues,
        };
        
        return View(model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Add() {
        var dto = new AddRepoDTO();
        return View(dto);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Add(AddRepoDTO model) {
        var userId = User.TryGetUserId();
        if (userId is null) {
            return Unauthorized();
        }

        bool isPublic = model.Visibility == Visibility.Public;
        
        Repository repo = new Repository(-1, userId.Value, model.Name, model.Description, isPublic, DateTime.Now);

        repo = repoService.AddRepo(repo);
        
        return RedirectToAction("Detail", new {id = repo.RepositoryId});
    }

    [HttpGet]
    [Authorize]
    public IActionResult Remove(long id) {
        var repo = repoService.GetRepo(id);
        if (repo is null) {
            return NotFound();
        }
        
        var userId = User.TryGetUserId();
        if (userId is null || userId != repo.OwnerId) {
            return Forbid();
        }

        RemoveRepoDTO dto = new RemoveRepoDTO() {
            RepositoryId = repo.RepositoryId,
            Name = repo.Name,
            Description = repo.Description,
        };
        return View(dto);
    }
    
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(RemoveRepoDTO dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        } 
        
        var repo = repoService.GetRepo(dto.RepositoryId);
        if (repo is null) {
            return NotFound();
        }
        
        var userId = User.TryGetUserId();
        if (userId is null || userId != repo.OwnerId) {
            return Forbid();
        }
        
        if (!repoService.RemoveRepo(dto.RepositoryId)) {
            return NotFound();
        }
        
        return RedirectToAction("Detail", "User", new {id = userId.Value});
    }

    [HttpGet]
    [Authorize]
    public IActionResult Edit(long id) {
        var repo = repoService.GetRepo(id);
        if (repo is null) {
            return NotFound();
        }
        
        if (repo.OwnerId != User.TryGetUserId()) {
            return Forbid();
        }

        EditRepoDTO dto = new EditRepoDTO() {
            RepositoryId = repo.RepositoryId,
            Name = repo.Name,
            Description = repo.Description,
            IsPublic = repo.IsPublic,
        };
        return View(dto);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditRepoDTO dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        } 
        
        var repo = repoService.GetRepo(dto.RepositoryId);
        if (repo is null) {
            return NotFound();
        }
        
        if (repo.OwnerId != User.TryGetUserId()) {
            return Forbid();
        }
        
        repo.Name = dto.Name;
        repo.Description = dto.Description;
        repo.IsPublic = dto.IsPublic;

        repo = repoService.UpdateRepo(repo);
        
        return RedirectToAction("Detail", new {id = repo!.RepositoryId});
    }

    [HttpGet]
    public IActionResult ListForUser(long userId) {
        User? user = userService.GetUserById(userId);
        if (user == null) {
            return NotFound();
        }
        
        List<Repository> repos = repoService.GetAllRepos().Where(r => r.OwnerId == userId).ToList();

        ListReposVM model = new ListReposVM() {
            User = user,
            Repos = repos,
            SearchForm = new SearchRepoForm(),
        };
        
        return View(model);
        return Ok("List out repositories for user with id: " + userId);
    }
}