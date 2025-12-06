using MiniGitHub.Data.Entities;
using MiniGitHub.Domain.Entities;
using DataIssueStatus = MiniGitHub.Data.Entities.IssueStatus;
using IssueStatus = MiniGitHub.Domain.Entities.IssueStatus;

namespace MiniGitHub.Domain.Mappers;

public class IssueMapper : IMapper<IssueRow, Issue> {
    private static DataIssueStatus MapIssueStatusToData(IssueStatus domain) {
        return (domain) switch {
            IssueStatus.Open => DataIssueStatus.Open,
            IssueStatus.Closed => DataIssueStatus.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(domain), domain, null)
        };
    }

    private IssueStatus MapIssueStatusFromData(DataIssueStatus data) {
        return (data) switch {
            DataIssueStatus.Open => IssueStatus.Open,
            DataIssueStatus.Closed => IssueStatus.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(data), data, null)
        };
    }
    
    public IssueRow MapToRow(Issue model) {
        IssueRow row = new IssueRow(
            model.IssueId, 
            model.RepositoryId, 
            model.CreatorId, 
            model.Title, 
            model.Description, 
            MapIssueStatusToData(model.Status), 
            model.CreatedAt, 
            model.ClosedAt);
        
        return row;
    }

    public Issue MapFromRow(IssueRow row) {
        Issue model = new Issue(
            row.IssueId, 
            row.RepositoryId, 
            row.CreatorId, 
            row.Title, 
            row.Description, 
            MapIssueStatusFromData(row.Status),
            row.CreatedAt,
            row.ClosedAt);
        
        return model;
    }
}