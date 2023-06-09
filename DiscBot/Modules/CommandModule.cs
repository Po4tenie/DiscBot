using DiscBot.Handlers;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria.Node;

namespace DiscBot.Modules
{
    public class CommandModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly LavaNode _lavaNode;

        public CommandModule(LavaNode lavaNode)
        {
            _lavaNode = lavaNode;
        }

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
            var voiceState = Context.User as IVoiceState;
            await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);
        }


    }
}
