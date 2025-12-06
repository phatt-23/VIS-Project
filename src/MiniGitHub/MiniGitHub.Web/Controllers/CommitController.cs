using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services;
using MiniGitHub.Web.Extensions;
using MiniGitHub.Web.Models;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Web.Controllers;

public class CommitController(
    IRepositoryService repoService,
    ICommitService commitService,
    IFileService fileService
) : Controller 
{
    [HttpGet]
    [Authorize]
    public IActionResult Add(long repoId) {
        Repository? repo = repoService.GetRepo(repoId);
        if (repo is null) {
            return NotFound();
        }

        if (User.TryGetUserId() != repo.OwnerId) {
            return Forbid();
        }
        
        var dto = new AddCommitDTO();
        dto.RepositoryId = repoId;
        return View(dto);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(AddCommitDTO dto) {
        if (!ModelState.IsValid) {
            return View();
        }
        
        Repository? repo = repoService.GetRepo(dto.RepositoryId);
        if (repo is null) return NotFound();
        if (User.TryGetUserId() != repo.OwnerId) return Forbid();
        
        try {
            Commit commit = new Commit(-1, dto.RepositoryId, dto.Message, DateTime.Now);
            
            IEnumerable<File> files = dto.Files.Select<IFormFile, File>(file => {
                StreamReader reader = new StreamReader(file.OpenReadStream());
                string content = reader.ReadToEnd();
                return new File(-1, -1, file.FileName, content);
            });
            
            commit = commitService.AddCommit(commit, files);
            return RedirectToAction("Detail", new {id = commit.CommitId});
        }
        catch {
            ModelState.AddModelError("Message", "Something went wrong");
            return RedirectToAction("Add", new {id = dto.RepositoryId}); 
        }
    }
    
    [HttpGet]
    public IActionResult Detail(long id) {
        Commit? commit = commitService.GetCommit(id);
        if (commit == null) {
            return NotFound();
        }
        
        return View(commit);
    }

    [HttpGet]
    public IActionResult List(long repoId) {
        Repository? repo = repoService.GetRepo(repoId);
        if (repo == null) {
            return NotFound();
        }
        
        List<Commit> commits = commitService.GetCommitsForRepo(repoId);

        CommitListVM vm = new CommitListVM() {
            Repo = repo,
            Commits = commits,
        };

        return View(vm);
    }
}
