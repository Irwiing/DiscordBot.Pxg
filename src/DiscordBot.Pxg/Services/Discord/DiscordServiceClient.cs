using Discord;
using Discord.WebSocket;
using DiscordBot.Pxg.Repositories.Pokemon;
using DiscordBot.Pxg.Services.Pokemon;

namespace DiscordBot.Pxg.Services.Discord;
    
public class DiscordServiceClient
{
    private DiscordSocketClient _client;
    private BoostServiceClient _boostServiceClient;
    
    private string _token;

    public DiscordServiceClient(string token)
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        _token = token;

        _boostServiceClient = new BoostServiceClient(new BoostRepository());
    }

    public async Task Connect()
    {
        _client.Ready += ReadyAsync;
        _client.Log += LogAsync;
        _client.MessageReceived += MessageReceivedAsync;

        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Author.Id == _client.CurrentUser.Id)
            return;

        if (message.Content.Contains("!boost"))
        {
            var @params = message.Content.Split(' ');

            var response = _boostServiceClient.VerifyAmountToBoost(int.Parse(@params[1]), int.Parse(@params[2]), int.Parse(@params[3]));

            await message.Channel.SendMessageAsync(response);
        }
    }

    private Task ReadyAsync()
    {
        Console.WriteLine($"{_client.CurrentUser} is connected!");

        return Task.CompletedTask;
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }
}
