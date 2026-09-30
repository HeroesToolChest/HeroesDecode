namespace HeroesDecode.Extensions;

public static class StormDraftPickExtensions
{
    extension(StormDraftPick stormDraftPick)
    {
        public DecodeDraftPick ToDecodeDraftPick()
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
}
