namespace HeroesDecode.Extensions;

public static class PlayerDisconnectExtensions
{
    public static DecodePlayerDisconnect ToDecodePlayerDisconnect(this PlayerDisconnect playerDisconnect)
    {
        DecodePlayerDisconnect decodePlayerDisconnect = new()
        {
            DisconnectTime = playerDisconnect.From,
            RejoinTime = playerDisconnect.To,
            LeaveReason = playerDisconnect.LeaveReason switch
            {
                null => "unknown",
                0 => "purposely left",
                11 or 12 => "disconnect",
                _ => $"unknown ({playerDisconnect.LeaveReason.Value})",
            },
        };

        return decodePlayerDisconnect;
    }
}
