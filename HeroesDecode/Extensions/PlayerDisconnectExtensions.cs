namespace HeroesDecode.Extensions;

public static class PlayerDisconnectExtensions
{
    extension(PlayerDisconnect playerDisconnect)
    {
        public DecodePlayerDisconnect ToDecodePlayerDisconnect()
        {
            DecodePlayerDisconnect decodePlayerDisconnect = new()
            {
                DisconnectTime = playerDisconnect.From,
                RejoinTime = playerDisconnect.To,
                LeaveReason = playerDisconnect.LeaveReason switch
                {
                    null => "unknown",
                    0 => "intentional",
                    11 or 12 => "disconnect",
                    _ => $"unknown ({playerDisconnect.LeaveReason.Value})",
                },
            };

            return decodePlayerDisconnect;
        }
    }
}
