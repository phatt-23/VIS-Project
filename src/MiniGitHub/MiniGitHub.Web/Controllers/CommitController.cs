using System.Data;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Data.DataConnector;
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

        try {
            Commit commit = new Commit(-1, dto.RepositoryId, dto.Message, DateTime.Now);
            var files =  dto.Files.Select(f => { f.CommitId = commit.CommitId; return f; }).ToList();
            
            commit = commitRepository.AddCommit(commit, files);
            return RedirectToAction("Detail", new {id = commit.CommitId});
        }
        catch {
            ModelState.AddModelError("Message", "Something went wrong");
            return RedirectToAction("Add", new {id = dto.RepositoryId}); 
        }
    }
    
    public IActionResult Detail(long id) {
        Commit? commit = commitRepository.GetCommit(id);
        if (commit == null) {
            return NotFound();
        }
        
        return View(commit);
    }
}
