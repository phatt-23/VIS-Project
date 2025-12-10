using System.Text.Json;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.TextDAOs;

public class CommitTextDao(string filepath) : ICommitDao {
    public CommitRow GetById(long id) {
        return GetAll().First(f => f.Id == id);
    }

    public List<CommitRow> GetAll() {
        var entries = JsonSerializer.Deserialize<List<CommitRow>>(File.ReadAllText(filepath));
        if (entries == null) {
            throw new Exception("Getting all files failed. There should be at least an empty array");
        }
        return entries;
    }

    public CommitRow Insert(CommitRow row) {
        var entries = GetAll();
        row.Id = entries.MaxBy(u => u.Id)?.Id + 1 ?? 0;
        entries.Add(row);
        string serialized = JsonSerializer.Serialize(entries);
        File.WriteAllText(filepath, serialized);
        return row;
    }

    public CommitRow Update(CommitRow entity) {
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

    public List<CommitRow> GetByRepoId(long repoId) {
        return GetAll().Where(f => f.RepositoryId == repoId).ToList();
    }
}