namespace HeroesDecode.Extensions;

public static class StormTrackerEventExtensions
{
    extension(StormTrackerEvent stormTrackerEvent)
    {
        public DecodeTrackerEvent ToDecodeTrackerEvent()
        {
            return new()
            {
                TrackerEventType = stormTrackerEvent.TrackerEventType,
                Timestamp = stormTrackerEvent.Timestamp,
                Data = stormTrackerEvent.VersionedDecoder?.ToJson(),
            };
        }
    }
}
