using AikaEmu.GameServer.Network;
using AikaEmu.GameServer.Network.GameServer;
using AikaEmu.Shared.Network;

namespace AikaEmu.GameServer.Packets.Game
{
    public class ResponseDeleteCharToken : GamePacket
    {
        private readonly int _result;

        public ResponseDeleteCharToken(int result)
        {
            _result = result;
            Opcode = (ushort) GameOpcode.ResponseDeleteCharToken;
        }

        public override PacketStream Write(PacketStream stream)
        {
            stream.Write(_result);
            stream.Write(0);
            return stream;
        }
    }
}