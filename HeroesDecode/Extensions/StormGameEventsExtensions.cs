namespace HeroesDecode.Extensions;

public static class StormGameEventsExtensions
{
    extension(StormGameEvent stormGameEvent)
    {
        public DecodeGameEvents ToDecodeGameEvents()
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
}
