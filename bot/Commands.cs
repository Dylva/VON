using DSharpPlus.Commands;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Media.Imaging;
using VON;
using DSharpPlus.Net;


namespace VON.commands
{
    [Command("math")]
    public class MathCommands
    {
        [Command("add")]
        public static ValueTask AddAsync(CommandContext ctx, int a, int b) => ctx.RespondAsync($"{a} + {b} = {a + b}");
        [Command("subtract")]
        public static ValueTask SubtractAsync(CommandContext ctx, int a, int b) => ctx.RespondAsync($"{a} - {b} = {a - b}");
    }

    public class PingCommand
    {
        [Command("ping")]
        public static ValueTask ExecuteAsync(CommandContext ctx) => ctx.RespondAsync("Guess I'll say pong!");
    }

    public class EmoteCommands
    {
        [Command("AddEmote")]
        public static async ValueTask ExecuteAsync(CommandContext ctx, string eName, string url)
        {
            await RestBot.AddEmote(eName, url);
            await ctx.RespondAsync($"Added {eName}");
        }
        [Command("DelEmote")]
        public static async ValueTask ExecuteAsync(CommandContext ctx, ulong emoteID)
        {
            await RestBot.DelEmote(emoteID, "");
            await ctx.RespondAsync("Emote deleted");
        }
    }
}