namespace HeroesDecode.Extensions;

public static class StormMessageExtensions
{
    public static DecodeMessage ToDecodeMessage(this IStormMessage stormMessageBase)
    {
        return new()
        {
            PlayerSender = stormMessageBase.MessageSender?.ToonHandle?.ToString(),
            Timestamp = stormMessageBase.Timestamp,
            MessageEventType = stormMessageBase.MessageEventType,
            Message = stormMessageBase.Message,
        };
    }
}
