using System.Text.Json;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.TextDAOs;

public class RepositoryTextDao(string path) : IRepositoryDao {
    
    public RepositoryRow GetById(long repoId) {
        var repos = GetAll().Where(r => r.Id == repoId).ToList();
        
        if (repos.Count == 0) {
            throw new Exception($"Repository with id {repoId} not found");
        }
        
        return repos.Single();
    }

    public List<RepositoryRow> GetByUserId(long userId) {
        var repos = GetAll().Where(r => r.OwnerId == userId).ToList();
        return repos;
    }

    public List<RepositoryRow> GetAll() {
        var repos = JsonSerializer.Deserialize<List<RepositoryRow>>(File.ReadAllText(path));
        
        if (repos == null) {
            throw new Exception("Getting all repositories failed. There should be at least an empty array");
        }

        return repos;
    }

    public RepositoryRow Insert(RepositoryRow row) {
        var entries = GetAll();
        row.Id = entries.MaxBy(u => u.Id)?.Id + 1 ?? 0;
        entries.Add(row);
        string serialized = JsonSerializer.Serialize(entries);
        File.WriteAllText(path, serialized);
        return row;
    }

    public bool Delete(long repoId) {
        var repos = GetAll();
        
        var repo = repos.SingleOrDefault(r => r.Id == repoId);
        if (repo == null) {
            return false;
        }

        repos.Remove(repo);
        
        string serialized = JsonSerializer.Serialize(repos);
        File.WriteAllText(path, serialized);

        return true;
    }

    public RepositoryRow Update(RepositoryRow repo) {
        var repos = GetAll();
        int index = repos.FindIndex(r => r.Id == repo.Id);
        repos[index] = repo;
        
        string serialized = JsonSerializer.Serialize(repos);
        File.WriteAllText(path, serialized);
        
        return repo;
    }
}
