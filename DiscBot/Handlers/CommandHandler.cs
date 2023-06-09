using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace DiscBot.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services) { 
           
            _client = client;
            _commands = commands;
            _services = services;
            
        }



        public async Task initializateAsync() {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _client.InteractionCreated += HandleIteraction;
        }

     
        
            private async Task HandleIteraction(SocketInteraction arg)
        {
            try
            {
                var msg = new SocketInteractionContext(_client, arg);
                await _commands.ExecuteCommandAsync(msg, _services);


            }
            catch(Exception ex) { 
                Console.WriteLine(ex.ToString());

                if (arg.Type == InteractionType.ApplicationCommand)
                    await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
            }
        
        }
    
    }
}
