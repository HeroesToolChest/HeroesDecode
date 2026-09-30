namespace HeroesDecode.Extensions;

public static class StormMessageExtensions
{
    extension(IStormMessage stormMessage)
    {
        public DecodeMessage ToDecodeMessage()
        {
            return new()
            {
                PlayerSender = stormMessage.MessageSender?.ToonHandle?.ToString(),
                Timestamp = stormMessage.Timestamp,
                MessageEventType = stormMessage.MessageEventType,
                Message = stormMessage.Message,
            };
        }
    }
}
