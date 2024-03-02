using DiscordBot.Pxg.Services.Discord;

namespace DiscordBot.Pxg;

public static class Program
{
    static async Task Main()
    {
        var discordServiceClient = new DiscordServiceClient(Environment.GetEnvironmentVariable("discordToken"));
        
        await discordServiceClient.Connect();
        Console.ReadKey();
    }
};