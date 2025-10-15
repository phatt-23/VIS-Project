using System.Data.Common;
using MiniGitHub.Data.Extensions;

namespace MiniGitHub.Data.Rows;

public class RepositoryRow {
    public int RepositoryId {get;set;}
    public int OwnerId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public bool IsPublic {get;set;}
    public DateTime CreatedAt {get;set;}

    public RepositoryRow() {
        // need this for deserealization, and optional list initializing of the object
    }
    
    public RepositoryRow(
        int ownerId,
        string name,
        string description,
        bool isPublic,
        DateTime createdAt) {
        
        OwnerId = ownerId;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        CreatedAt = createdAt;
    }

    public RepositoryRow(DbDataReader reader) {
        RepositoryId = reader.Get<int>("repository_id");
        OwnerId = reader.Get<int>("owner_id");
        Name = reader.Get<string>("name");
        Description = reader.Get<string>("description");
        IsPublic = reader.Get<bool>("is_public");
        CreatedAt = reader.Get<DateTime>("created_at");
    }

    public override string ToString() {
        return $"RepositoryId: {RepositoryId}, OwnerId: {OwnerId}, Name: {Name}, Description: {Description}, IsPublic: {IsPublic}, CreatedAt: {CreatedAt}";
    }
}
