using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.Rows;

public class RepositoryRow : IIdentifialbeRow {
    public long Id {get;set;}
    public long OwnerId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public bool IsPublic {get;set;}
    public DateTime CreatedAt {get;set;}

    public RepositoryRow() {
        // need this for deserialization and optional list initializing of the object
    }
    
    public RepositoryRow(
        long ownerId,
        string name,
        string description,
        bool isPublic,
        DateTime createdAt
    ) {
        
        OwnerId = ownerId;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        CreatedAt = createdAt;
    }

    public RepositoryRow(DbDataReader reader) {
        Id = reader.Get<long>("repository_id");
        OwnerId = reader.Get<long>("owner_id");
        Name = reader.Get<string>("name");
        Description = reader.Get<string>("description");
        IsPublic = reader.Get<bool>("is_public");
        CreatedAt = reader.Get<DateTime>("created_at");
    }

    public override string ToString() {
        return $"RepositoryId: {Id}, OwnerId: {OwnerId}, Name: {Name}, Description: {Description}, IsPublic: {IsPublic}, CreatedAt: {CreatedAt}";
    }
}
