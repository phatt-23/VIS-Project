using System.Text.Json;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.DAOs.TextDAOs;

public class IssueTextDao(string filepath) : IIssueDao  {
    public IssueRow GetById(long id) {
        return GetAll().First(f => f.Id == id);
    }

    public List<IssueRow> GetAll() {
        var data = JsonSerializer.Deserialize<List<IssueRow>>(File.ReadAllText(filepath));
        if (data == null) {
            throw new Exception("Getting all issues failed. There should be at least an empty array");
        }
        return data;
    }

    public IssueRow Insert(IssueRow row) {
        var entries = GetAll();
        row.Id = entries.MaxBy(u => u.Id)?.Id + 1 ?? 0;
        entries.Add(row);
        string serialized = JsonSerializer.Serialize(entries);
        File.WriteAllText(filepath, serialized);
        return row;
    }

    public IssueRow Update(IssueRow entity) {
        var data = GetAll();
        var index = data.FindIndex(f => f.Id == entity.Id);
        data[index] = entity;
        File.WriteAllText(filepath, JsonSerializer.Serialize(data));
        return entity;
    }

    public bool Delete(long id) {
        var data = GetAll();
        var it = data.SingleOrDefault(r => r.Id == id);
        if (it == null) {
            return false;
        }
        data.Remove(it);
        File.WriteAllText(filepath, JsonSerializer.Serialize(data));
        return true;        
    }

    public List<IssueRow> GetByRepoId(long repoId) {
        return GetAll().Where(f => f.RepositoryId == repoId).ToList();
    }
}