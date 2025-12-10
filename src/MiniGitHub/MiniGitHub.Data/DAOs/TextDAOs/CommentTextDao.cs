using System.Text.Json;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.TextDAOs;

public class CommentTextDao(string filepath) : ICommentDao {
    
    public CommentRow GetById(long id) {
        return GetAll().First(f => f.Id == id);
    }

    public List<CommentRow> GetAll() {
        var entries = JsonSerializer.Deserialize<List<CommentRow>>(File.ReadAllText(filepath));
        if (entries == null) {
            throw new Exception("Getting all files failed. There should be at least an empty array");
        }
        return entries;
    }

    public CommentRow Insert(CommentRow row) {
        var entries = GetAll();
        row.Id = entries.MaxBy(u => u.Id)?.Id + 1 ?? 0;
        entries.Add(row);
        string serialized = JsonSerializer.Serialize(entries);
        File.WriteAllText(filepath, serialized);
        return row;
    }

    public CommentRow Update(CommentRow entity) {
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
}