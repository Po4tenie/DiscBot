using DiscBot.Handlers;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscBot.Modules
{
    public class CommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        
        [SlashCommand("киса", "Милая кошка на фотографии")]
        public async Task kisaa()
        {
            var embed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Наслаждайся")
                    .WithImageUrl(CatHandler.GetCat())
                    .WithCurrentTimestamp()
                    .WithFooter(footer => footer.Text = "Спать хочется");
            await RespondAsync(embed: embed.Build());
        }


    }
}
