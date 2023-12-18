namespace HeroesDecode.Extensions;

public static class StormTrackerEventExtensions
{
    public static DecodeTrackerEvent ToDecodeTrackerEvent(this StormTrackerEvent stormTrackerEvent)
    {
        return new()
        {
            TrackerEventType = stormTrackerEvent.TrackerEventType,
            Timestamp = stormTrackerEvent.Timestamp,
            Data = stormTrackerEvent.VersionedDecoder?.ToJson(),
        };
    }
}
