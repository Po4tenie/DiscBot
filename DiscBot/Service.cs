using DiscBot.Handlers;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Victoria.Node;
using Victoria;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using DiscBot.Modules;
using Discord.Interactions;

namespace DiscBot
{
    public class Service
    {
        public readonly DiscordSocketClient _client;
        private readonly PrefixHandler _prefixHandler;
        private readonly CommandService _commandService;
        private readonly InteractionService _commands;
        private readonly ServiceProvider _services;
        private readonly LavaNode _lavaNode;
        private readonly AudioService _audioService;



        public Service()
        {

            _services = ConfigureServices();
            _client = _services.GetRequiredService<DiscordSocketClient>();
            _commands = _services.GetRequiredService<InteractionService>();
            _prefixHandler = _services.GetRequiredService<PrefixHandler>();
            _lavaNode = _services.GetRequiredService<LavaNode>();
            _audioService = _services.GetRequiredService<AudioService>();
            SubscribeDiscordEvents();
            var config = new ConfigurationBuilder()
                 .SetBasePath(AppContext.BaseDirectory)
                 .AddYamlFile("config.yml")
                 .Build();
            _client.Log += async (LogMessage msg) => Console.WriteLine(msg.Message);
        }

        public async Task InitializeAsync()
        {

            var config = new ConfigurationBuilder()
                 .SetBasePath(AppContext.BaseDirectory)
                 .AddYamlFile("config.yml")
                 .Build();
            await _client.LoginAsync(TokenType.Bot, config["tokens:discord"]);
            await _client.StartAsync();
            await _services.GetRequiredService<CommandHandler>().initializateAsync();
            await _prefixHandler.InitializeAsync();
            _commands.RegisterCommandsToGuildAsync(UInt64.Parse(config["testguild"]));
            await Task.Delay(-1);


        }

        private void SubscribeDiscordEvents()
        {
            _client.Ready += ReadyAsync;
            
        }


        private async Task ReadyAsync()
        {
          
            await _lavaNode.ConnectAsync();

        }

      

        private ServiceProvider ConfigureServices()
        {
            
            return new ServiceCollection()
                .AddLogging()
               .AddSingleton(x => new CommandService(new CommandServiceConfig
               {
                   LogLevel = LogSeverity.Debug,
                   DefaultRunMode = Discord.Commands.RunMode.Async
               }))
                
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))         
                .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.All | GatewayIntents.MessageContent,
                    LogGatewayIntentWarnings = false,
                    AlwaysDownloadUsers = true,
                    LogLevel = LogSeverity.Debug
                }))
                .AddSingleton<PrefixHandler>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<LavaNode>()
                .AddSingleton<AudioService>()
                .AddSingleton<NodeConfiguration>()
                .AddLavaNode(x =>
                {
                    x.SelfDeaf = true;
                })
                .AddTransient<HttpClient>()               
                .BuildServiceProvider();
        }

       

    }
}
