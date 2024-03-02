namespace DiscordBot.Pxg.Interfaces.Repositories;

public interface IBoostRepository
{
    public Dictionary<int, Dictionary<int, int>> GetTiersTable();
}
