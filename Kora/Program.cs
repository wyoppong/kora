using Kora;
using Coravel;
using Kora.Database;
using Kora.Services;
using Kora.Settings;
using Kora.Interfaces;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=/Users/wisdomoppongyeboah/Documents/Database/kora.db";
builder.Services.AddSqlite<ApplicationDbContext>(connectionString);
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<Worker>();
builder.Services.AddScheduler();

var host = builder.Build();
host.Services.UseScheduler(s => s.Schedule<Worker>().EveryMinute());
host.Run();