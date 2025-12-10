using System.Text.Json;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.TextDAOs;

public class UserTextDao(string path) : IUserDao {
    public UserRow GetById(long userId) {
        var user = GetAll().FirstOrDefault(u => u.Id == userId);
        
        if (user == null) {
            throw new InvalidOperationException($"User with id {userId} wasn't found");
        }

        return user;
    }

    public List<UserRow> GetAll() {
        List<UserRow>? users = JsonSerializer.Deserialize<List<UserRow>>(File.ReadAllText(path));
        
        if (users == null) {
            throw new InvalidOperationException($"File with users wasn't read properly: {path}");
        }

        return users;
    }

    public UserRow GetByEmail(string email) {
        var user = GetAll().FirstOrDefault(u => u.Email == email);
        if (user == null) {
            throw new InvalidOperationException($"User with email {email} wasn't found");
        }

        return user;
    }

    public UserRow GetByUsername(string username) {
        var user = GetAll().FirstOrDefault(u => u.Username == username);
        
        if (user == null) {
            throw new InvalidOperationException($"User with username {username} wasn't found");
        }

        return user;
    }

    public UserRow Insert(UserRow row) {
        var entries = GetAll();
        row.Id = entries.MaxBy(u => u.Id)?.Id + 1 ?? 0;
        entries.Add(row);
        string serialized = JsonSerializer.Serialize(entries);
        File.WriteAllText(path, serialized);
        return row;
    }

    public UserRow Update(UserRow row) {
        var rows = GetAll();
        int index = rows.FindIndex(r => r.Id == row.Id);
        rows[index] = row;
        
        string serialized = JsonSerializer.Serialize(rows);
        File.WriteAllText(path, serialized);
        
        return row;
    }

    public bool Delete(long userId) {
        var users = GetAll();

        UserRow? user = users.SingleOrDefault(user => user.Id == userId);
        if (user is null) {
            return false;
        }

        users.Remove(user);
        
        string serialized = JsonSerializer.Serialize(users);
        File.WriteAllText(path, serialized);

        return true;
    }
}
