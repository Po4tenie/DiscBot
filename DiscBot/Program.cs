using DiscBot;
using DiscBot.Handlers;
using DiscBot.Modules;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;
using Victoria;
using Victoria.Node;

namespace Discbot { 


    public class MainClass {

        private static Task Main()
              => new Service().InitializeAsync();
    }
}