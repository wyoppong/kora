namespace Kora.Models;

public record MailDataModel
{
    public required string EmailToId { get; init; }
    public required string EmailToName { get; init; }
    public required string EmailSubject { get; init; }
    public required string EmailBody { get; init; }
}