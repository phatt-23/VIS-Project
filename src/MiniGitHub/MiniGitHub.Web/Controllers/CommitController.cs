using System.Data;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Repositories;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class CommitController(
    IRepositoryRepository repoRepository,
    ICommitRepository commitRepository,
    IFileRepository fileRepository
) : Controller 
{
    [HttpGet]
    public IActionResult Add(int repoId) {
        var dto = new AddCommitDTO();
        dto.RepositoryId = repoId;
        return View(dto);
    }

    [HttpPost]
    public IActionResult Add(AddCommitDTO dto) {
        Commit commit = new Commit(-1, dto.RepositoryId, dto.Message, DateTime.Now);
        commit = commitRepository.AddCommit(commit);

        foreach (var file in dto.Files) {
            file.CommitId = commit.CommitId;
            fileRepository.AddFile(file);
        }

        return RedirectToAction("Detail", new {id = commit.CommitId});
    }
    
    public IActionResult Detail(long id) {
        Commit? commit = commitRepository.GetCommit(id);
        if (commit == null) {
            return NotFound();
        }
        
        return View(commit);
    }
}
