using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Haiku.Bot.Handlers.CommandHandling
{
    public class CommandParameters
    {
        public string[] TextParams { get; set; } = Array.Empty<string>();

        public Update Update { get; set; } = null!;
    }
}
