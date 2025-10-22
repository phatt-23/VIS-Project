namespace MiniGitHub.Domain.Mappers;

public interface IMapper<Row, DomainModel> {
    Row MapToRow(DomainModel model);
    DomainModel MapFromRow(Row row); 
}