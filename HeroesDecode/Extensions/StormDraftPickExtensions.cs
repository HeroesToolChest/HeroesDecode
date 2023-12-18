namespace HeroesDecode.Extensions;

public static class StormDraftPickExtensions
{
    public static DecodeDraftPick ToDecodeDraftPick(this StormDraftPick stormDraftPick)
    {
        return new()
        {
            PickType = stormDraftPick.PickType,
            Player = stormDraftPick.Player?.ToonHandle?.ToString(),
            Team = stormDraftPick.Team,
            SelectedHeroId = stormDraftPick.HeroSelected,
        };
    }
}
