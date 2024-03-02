using Discord;
using Discord.WebSocket;

namespace DiscordBot.Pxg.Services;
    
public class DiscordServiceClient
{
    private DiscordSocketClient _client;
    
    private string _token;

    public DiscordServiceClient(string token)
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        _token = token;
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


        if (message.Content == "!ping")
        {
            var cb = new ComponentBuilder()
                .WithButton("Click me!", "unique-id", ButtonStyle.Primary);

            await message.Channel.SendMessageAsync("pong!", components: cb.Build());
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
