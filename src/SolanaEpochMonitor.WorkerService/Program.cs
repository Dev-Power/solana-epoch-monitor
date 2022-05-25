using SolanaEpochMonitor.WorkerService;
using SolanaEpochMonitor.EmailService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        
        services.AddHostedService<Worker>();
        services.AddSingleton<IEmailService, EmailService>();
        
        services.AddSingleton<IEmailSettings>(x => configuration.GetSection("EmailSettings").Get<EmailSettings>());
        services.AddSingleton<ISmtpSettings>(x => configuration.GetSection("SmtpSettings").Get<SmtpSettings>());
        services.AddSingleton<AppSettings>(x => configuration.GetSection("AppSettings").Get<AppSettings>());

    })
    .Build();

await host.RunAsync();