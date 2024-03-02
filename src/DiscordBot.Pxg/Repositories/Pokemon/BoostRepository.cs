using DiscordBot.Pxg.Interfaces.Repositories;

namespace DiscordBot.Pxg.Repositories.Pokemon;

public class BoostRepository : IBoostRepository
{
    public BoostRepository()
    {
        _tableOfTiers = CreateTiersTable();
    }

    private const int MAX_BOOST_LEVEL = 50;

    private readonly int[] _availableTiers = [2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 50];

    private Dictionary<int, Dictionary<int, int>> _tableOfTiers;

    public Dictionary<int, Dictionary<int, int>> GetTiersTable()
    {
        return _tableOfTiers;
    }

    private Dictionary<int, Dictionary<int, int>> CreateTiersTable()
    {
        var tiersTable = new Dictionary<int, Dictionary<int, int>>();

        foreach (var tier in _availableTiers)
        {
            tiersTable.Add(tier, CreateBoostTable(tier));
        }

        return tiersTable;
    }

    static private Dictionary<int, int> CreateBoostTable(int tier)
    {
        var boostTable = new Dictionary<int, int>();
        var totalStones = 0;

        var neededStones = 1;
        var stepToIncrease = 0;

        for (int boostLevel = 1; boostLevel <= MAX_BOOST_LEVEL; boostLevel++)
        {
            totalStones += neededStones;
            boostTable.Add(boostLevel, totalStones);

            stepToIncrease++;

            if(stepToIncrease == tier)
            {
                stepToIncrease = 0;
                neededStones++;
            }
        }

        return boostTable;
    }
}
