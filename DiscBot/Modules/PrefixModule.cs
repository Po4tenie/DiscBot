using Discord;
using Discord.Commands;
using DiscBot.Handlers;
using Victoria.Node;
using Discord.WebSocket;

namespace DiscBot.Modules
{
    public class PrefixModule : ModuleBase<SocketCommandContext>
    {
        //private readonly MusicHandler _musicHandler;
        //public PrefixModule(MusicHandler musicHandler)
        //{
        //    _musicHandler = musicHandler;
        //}
        //[Command("ы")]
        //public async Task JoinAsync111()
        //{

        //    var user = Context.User as SocketGuildUser;
        //    await _musicHandler.JoinAsync(user.VoiceChannel, Context.Channel as ITextChannel);
        //}
        [Command("кто")]
        public async Task kto()
        {
            var embed = new EmbedBuilder()
                .WithAuthor(Context.Message.Author)
                .WithColor(Color.Red)
                .WithTitle("Собсна, кто я вообще такой")
                .WithDescription("Я бот, который придуман от нечего делать и обновляется по такому же принципу")
                .WithCurrentTimestamp();
            embed.AddField("Что я могу?", "Вопрос хороший. Это поле будет дополняться со временем")
            .WithFooter(footer => footer.Text = "спать хочется");


            await ReplyAsync(embed: embed.Build());

        }
        [Command("киса")]

        public async Task kisaa()
        {
            var embed = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Наслаждайся")
                    .WithImageUrl(CatHandler.GetCat())
                    .WithCurrentTimestamp()
                    .WithFooter(footer => footer.Text = "спать хочется");
            await ReplyAsync(embed: embed.Build());


            var voiceState = Context.User as IGuildUser;
            //await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);
        }
    }
}
