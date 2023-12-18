namespace HeroesDecode.Models;

public class DecodeDraftPick
{
    public string? Player { get; set; }

    public string SelectedHeroId { get; set; } = string.Empty;

    public StormTeam Team { get; set; }

    public StormDraftPickType PickType { get; set; }
}
