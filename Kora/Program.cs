using Kora;
using Coravel;
using Kora.Services;
using Kora.Settings;
using Kora.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<Worker>();
builder.Services.AddScheduler();

var host = builder.Build();
host.Services.UseScheduler(s => s.Schedule<Worker>().EveryFifteenMinutes());
host.Run();