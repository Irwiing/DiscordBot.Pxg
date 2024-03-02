using DiscordBot.Pxg.Interfaces.Repositories;

namespace DiscordBot.Pxg.Services.Pokemon;
public class BoostServiceClient
{
    private readonly IBoostRepository _repository;

    public BoostServiceClient(IBoostRepository repository)
    {
        _repository = repository;
    }
   
   public string VerifyAmountToBoost(int tier, int level, int target)
   {
        var tiersTable = _repository.GetTiersTable();

        var usedStones = tiersTable[tier][level];
        var targetStones = tiersTable[tier][target] - tiersTable[tier][level];

        return  $"Tier: **{tier}** " +
                $"\nBoost: **+{level}** " +
                $"\nAté agora você gastou: **{usedStones}** stone(s)" + 
                $"\nPara alcançar **+{target}** " + 
                $"você precisará de **{targetStones}** stone(s).";
   }
}