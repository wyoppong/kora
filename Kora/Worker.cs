using Bogus;
using Coravel.Invocable;
using Kora.Database;
using Kora.Database.Entities;
using Kora.Enums;
using Kora.Interfaces;
using Kora.Models;

namespace Kora;

public class Worker(ILogger<Worker> logger, IMailService _mailer, ApplicationDbContext _context) : IInvocable
{
    public async Task Invoke()
    {
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        await SeedUsersAsync();
        await Task.Delay(1000);
    }

    private async Task SeedUsersAsync()
    {
        var testUsers = new Faker<User>()
            //Basic rules using built-in generators
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Id, Guid.NewGuid)

            //Use an enum outside scope.
            .RuleFor(u => u.Sex, f => f.PickRandom<Sex>())
            .FinishWith((f, u) =>
            {
                logger.LogInformation("User Created! Id={0}", u.Id);
            });

        var user = testUsers.Generate();

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    private async Task SendEmailAsync()
    {
        var mailData = new MailDataModel
        {
            EmailToName = "Wisdom Oppong Yeboah",
            EmailToId = "woppongyeboah@firstatlanticbank.com.gh",
            EmailSubject = "Testing Coravel Service",
            EmailBody = "<p>Testing the coravel service</p>"
        };

        await _mailer.DispatchMailAsync(mailData);
    }
}