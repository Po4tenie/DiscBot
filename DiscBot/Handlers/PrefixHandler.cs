using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiscBot.Handlers
{
    public class PrefixHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _services;

        public PrefixHandler(DiscordSocketClient client, CommandService command, IServiceProvider services) { 
        _client = client;
        _command = command;
        _services = services;
        HookEvents();
        }

        public async Task InitializeAsync() {
            await _command.AddModulesAsync(
                assembly: Assembly.GetEntryAssembly(),
                services: _services);
        }
        public void HookEvents()
        {
            //_command.Log += LogAsync;
            _client.MessageReceived += HandlerCommandAsync;
        }

        public async Task HandlerCommandAsync(SocketMessage messageP) {
            var config = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddYamlFile("config.yml")
              .Build();
            var message = messageP as SocketUserMessage;
            if (message == null) { return; }
            int argPos = 0;
            
            if (!(message.HasStringPrefix(config["prefix"], ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot) { return; }


                var context = new SocketCommandContext(_client, message);
                await _command.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: null
                    );
        
        }
    }
}
