namespace HeroesDecode.Extensions;

public static class StormGameEventsExtensions
{
    public static DecodeGameEvents ToDecodeGameEvents(this StormGameEvent stormGameEvent)
    {
        return new()
        {
            Player = stormGameEvent.MessageSender?.ToonHandle?.ToString(),
            GameEventType = stormGameEvent.GameEventType,
            TimeStamp = stormGameEvent.Timestamp,
            Data = stormGameEvent.Data?.ToJson(),
        };
    }
}
