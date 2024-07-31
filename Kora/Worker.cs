using Coravel.Invocable;
using Kora.Interfaces;
using Kora.Models;

namespace Kora;

public class Worker(ILogger<Worker> logger, IMailService _mailer) : IInvocable
{
    public async Task Invoke()
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        }

        var mailData = new MailDataModel
        {
            EmailToName = "Wisdom Oppong Yeboah",
            EmailToId = "woppongyeboah@firstatlanticbank.com.gh",
            EmailSubject = "Testing Coravel Service",
            EmailBody = "<p>Testing the coravel service</p>"
        };

        await _mailer.DispatchMailAsync(mailData);

        await Task.Delay(1000);
    }
}