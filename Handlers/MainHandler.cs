using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Haiku.Bot.Services;

namespace Haiku.Bot.Handlers;

public class MainHandler
{
    private readonly ILogger<TelegramWorker> _logger;

    public MainHandler(ILogger<TelegramWorker> logger)
    {
        _logger = logger;
    }


    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        _logger.LogInformation("Update recieved");


        throw new NotImplementedException();
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception arg2, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
