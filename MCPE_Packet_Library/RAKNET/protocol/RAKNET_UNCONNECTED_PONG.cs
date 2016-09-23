using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_UNCONNECTED_PONG : RAKNET_BASE
    {
        public static byte ID = (byte)0x1c;

        public override byte getID()
        {
            return ID;
        }

        public long pingID;
        public long serverID;
        public string serverName;

        public override void encode()
        {
            base.encode();
            this.putLong(this.pingID);
            this.putLong(this.serverID);
            this.put(RAKNET.MAGIC);
            this.putString(this.serverName);
        }

        public override void decode()
        {
            base.decode();
            this.pingID = this.getLong();
            this.serverID = this.getLong();
            this.offset += 16;//magic
            this.serverName = serverName = this.getString();
        }
    }
}
