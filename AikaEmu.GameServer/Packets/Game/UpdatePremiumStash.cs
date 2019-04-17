using AikaEmu.GameServer.Network;
using AikaEmu.GameServer.Network.GameServer;
using AikaEmu.Shared.Network;

namespace AikaEmu.GameServer.Packets.Game
{
    public class UpdatePremiumStash : GamePacket
    {
        public UpdatePremiumStash()
        {
            Opcode = (ushort) GameOpcode.UpdatePremiumStash;
        }

        public override PacketStream Write(PacketStream stream)
        {
            for (var i = 0; i < 48; i++)
            {
                stream.Write(0);
            }

            return stream;
        }
    }
}