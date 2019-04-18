using AikaEmu.GameServer.Network.GameServer;
using AikaEmu.Shared.Network;

namespace AikaEmu.GameServer.Packets.Client
{
	public class MoveUnit : GamePacket
	{
		protected override void Read(PacketStream stream)
		{
			var coordX = stream.ReadSingle();
			var coordY = stream.ReadSingle();
			var unk1 = stream.ReadInt32();
			var unk2 = stream.ReadInt16();
			var unk3 = stream.ReadByte();
			var speed = stream.ReadByte();
			var unk4 = stream.ReadInt32();
			//Log.Debug("MoveUnit");

			// TODO - Improve this function
			Connection.ActiveCharacter.SetPosition(coordX, coordY);
		}
	}
}