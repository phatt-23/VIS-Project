using System.Text.Json;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects.TextDAOs;

public class RepositoryTextDao : IRepositoryDao {
    public RepositoryTextDao(string path) {
        _path = path;
    }

    public RepositoryRow? GetById(int repoId) {
        var repos = GetAll().Where(r => r.RepositoryId == repoId).ToList();
        if (!repos.Any()) {
            return null;
        }
        
        return repos.Single();
    }

    public List<RepositoryRow> GetByUserId(int userId) {
        var repos = GetAll().Where(r => r.OwnerId == userId).ToList();
        return repos;
    }

    public List<RepositoryRow> GetAll() {
        var repos = JsonSerializer.Deserialize<List<RepositoryRow>>(File.ReadAllText(_path));
        if (repos == null) {
            throw new Exception(
                    "Getting all repositories failed. There should be at least an empty array");
        }

        return repos;
    }

    public RepositoryRow Insert(RepositoryRow row) {
        var repos = GetAll();

        row.RepositoryId = repos.Count;
        repos.Add(row);
        
        string serialized = JsonSerializer.Serialize(repos);
        File.WriteAllText(_path, serialized);
        return row; 
    }

    private readonly string _path;
}
