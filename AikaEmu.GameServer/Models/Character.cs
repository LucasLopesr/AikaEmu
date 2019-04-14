using System;
using AikaEmu.GameServer.Models.Base;
using AikaEmu.GameServer.Models.Char;
using AikaEmu.GameServer.Network.GameServer;
using NLog;

namespace AikaEmu.GameServer.Models
{
    public class Character : BaseUnit
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        public uint AccountId { get; set; }
        public GameConnection Connection { get; set; }
        public ushort ConnectionId => Connection.Account.ConnectionId;

        public uint Slot { get; set; }
        public ushort Level { get; set; }
        public CharAttributes CharAttributes { get; set; }
        public CharClass CharClass { get; set; }
        public CharFace Face { get; set; }
        public CharHair Hair { get; set; }
        public ulong Money { get; set; }
        public ulong Experience { get; set; }
        public int HonorPoints { get; set; }
        public int PvpPoints { get; set; }
        public int InfamyPoints { get; set; }
        public string Token { get; set; }

        public bool Save()
        {
            using (var sql = GameServer.Instance.DatabaseManager.GetConnection())
            using (var transaction = sql.BeginTransaction())
            {
                using (var command = sql.CreateCommand())
                {
                    command.CommandText =
                        "REPLACE INTO `characters`" +
                        "(`id`,`acc_id`, `slot`, `name`, `level`, `class`, `width`, `chest`, `leg`, `body`, `exp`, `face`, `hair`, `money`, `hp`, `mp`, `x`, `y`, `honor_point`, `pvp_point`, `infamy_point`, `str`, `agi`, `int`, `const`, `spi`, `token`, `updated_at`)" +
                        "VALUES (@id, @acc_id, @slot, @name, @level, @class, @width, @chest, @leg, @body, @exp, @face, @hair, @money, @hp, @mp, @x, @y, @honor, @pvp, @infamy, @str, @agi, @int, @const, @spi, @token, @updated_at)";

                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@acc_id", AccountId);
                    command.Parameters.AddWithValue("@slot", Slot);
                    command.Parameters.AddWithValue("@name", Name);
                    command.Parameters.AddWithValue("@level", Level);
                    command.Parameters.AddWithValue("@class", (ushort) CharClass);
                    command.Parameters.AddWithValue("@width", BodyTemplate.Width);
                    command.Parameters.AddWithValue("@chest", BodyTemplate.Chest);
                    command.Parameters.AddWithValue("@leg", BodyTemplate.Leg);
                    command.Parameters.AddWithValue("@body", BodyTemplate.Body);
                    command.Parameters.AddWithValue("@exp", Experience);
                    command.Parameters.AddWithValue("@face", (ushort) Face);
                    command.Parameters.AddWithValue("@hair", (ushort) Hair);
                    command.Parameters.AddWithValue("@money", Money);
                    command.Parameters.AddWithValue("@hp", Hp);
                    command.Parameters.AddWithValue("@mp", Mp);
                    command.Parameters.AddWithValue("@x", Position.CoordX);
                    command.Parameters.AddWithValue("@y", Position.CoordY);
                    command.Parameters.AddWithValue("@honor", HonorPoints);
                    command.Parameters.AddWithValue("@pvp", PvpPoints);
                    command.Parameters.AddWithValue("@infamy", InfamyPoints);
                    command.Parameters.AddWithValue("@str", CharAttributes.Strenght);
                    command.Parameters.AddWithValue("@agi", CharAttributes.Agility);
                    command.Parameters.AddWithValue("@int", CharAttributes.Intelligence);
                    command.Parameters.AddWithValue("@const", CharAttributes.Constitution);
                    command.Parameters.AddWithValue("@spi", CharAttributes.Spirit);
                    command.Parameters.AddWithValue("@token", Token);
                    command.Parameters.AddWithValue("@updated_at", DateTime.UtcNow);
                    command.ExecuteNonQuery();
                }

                try
                {
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    _log.Error(e);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exception)
                    {
                        _log.Error(exception);
                    }

                    return false;
                }
            }
        }
    }
}