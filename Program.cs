using Haiku.Bot;

var builder = Host.CreateDefaultBuilder(args);

var host = builder
    .ConfigureServices((context, services) =>
    {
        services.AddConfiguration(context.Configuration);


        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
