using System;
using DSharpPlus;
using DSharpPlus.Commands;
using System.Threading.Tasks;
using DSharpPlus.Commands.Trees;
using DSharpPlus.Commands.Processors.TextCommands;
using DSharpPlus.Commands.Processors.TextCommands.Parsing;
using DSharpPlus.Entities;
using DSharpPlus.Net;
using System.IO;
using System.Net.Http;
using bot.config;


namespace VON
{
    public class Bot
    {
        public static async Task BotMain()
        {
            var config = new Config();
            await config.ReadConfig();

            DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(config.token, DiscordIntents.AllUnprivileged);
            DiscordClient client = builder.Build();

            CommandsExtension commandsExtension = client.UseCommands(new CommandsConfiguration()
            {
                DebugGuildId = config.guildId,
                RegisterDefaultCommandProcessors = true
            });

            commandsExtension.AddCommands(typeof(Bot).Assembly);
            TextCommandProcessor txtCmdProcessor = new(new()
            {
                PrefixResolver = new DefaultPrefixResolver(true, ",").ResolvePrefixAsync
            });

            commandsExtension.AddProcessors(txtCmdProcessor);
            DiscordActivity status = new DiscordActivity("the world burn", DiscordActivityType.ListeningTo);


            await client.ConnectAsync(status, DiscordUserStatus.Online);
            await Task.Delay(-1);

        }
    }

    public class RestBot
    {
        private static DiscordRestClient rClient = new DiscordRestClient(new RestClientOptions(), "token", TokenType.Bot);

        public static async Task AddEmote(string eName, string url)
        {
            Stream stream;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                stream = await response.Content.ReadAsStreamAsync();
            }
            // 123456789012345678 = Guild ID
            await rClient.CreateEmojiAsync(123456789012345678, eName, stream);

        }

        public static async Task DelEmote(ulong emoteID, string reason)
        {
            // 123456789012345678 = Guild ID
            await rClient.DeleteGuildEmojiAsync(123456789012345678, emoteID, reason);
        }
    }

}