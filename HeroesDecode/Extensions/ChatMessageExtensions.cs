namespace HeroesDecode.Extensions;

public static class ChatMessageExtensions
{
    extension(ChatMessage chatMessage)
    {
        public DecodeTeamChatMessage ToDecodeTeamChatMessage()
        {
            return new()
            {
                PlayerSender = chatMessage.MessageSender?.ToonHandle?.ToString(),
                Timestamp = chatMessage.Timestamp,
                MessageTarget = chatMessage.MessageTarget,
                Text = chatMessage.Text,
            };
        }
    }
}
