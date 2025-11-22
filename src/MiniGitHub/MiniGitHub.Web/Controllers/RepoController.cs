using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Repositories;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class RepoController(
    IRepositoryRepository repoRepository,
    IFileRepository fileRepository
) : Controller {
    // GET
    public IActionResult Index() {
        return View();
    }

    public IActionResult Detail(long id) {
        Repository? repo = repoRepository.GetRepo(id);
        if (repo == null) {
            return NotFound();
        }
        
        foreach (var commit in repo.Commits)
        {
            commit.Files = fileRepository.GetByCommitId(commit.CommitId);
        }

        return View(repo);
    }

    
    [HttpGet]
    public IActionResult Add(long ownerId) {
        var dto = new AddRepositoryDTO();
        dto.OwnerId = ownerId;
        return View(dto);
    }
    
    [HttpPost]
    public IActionResult Add([FromForm] AddRepositoryDTO dto) {
        
    // public Repository(long repositoryId, long ownerId, string name, string description, bool isPublic, DateTime createdAt) {
        Repository repo = new Repository(-1, dto.OwnerId, dto.Name, dto.Description, dto.IsPublic, DateTime.Now);

        repo = repoRepository.AddRepo(repo);
        
        return RedirectToAction("Detail", new {id = repo.RepositoryId});
    }
}