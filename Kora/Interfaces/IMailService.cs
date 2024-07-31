using Kora.Models;

namespace Kora.Interfaces;

public interface IMailService
{
    Task<bool> DispatchMailAsync(MailDataModel model);
}