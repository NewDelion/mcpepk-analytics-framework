using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol.DataPacket
{
    public class RAKNET_PING : RAKNET_BASE
    {
        public static byte ID = (byte)0x00;

        public override byte getID()
        {
            return ID;
        }

        public long pingID;

        public override void encode()
        {
            base.encode();
            this.putLong(this.pingID);
        }

        public override void decode()
        {
            base.decode();
            this.pingID = this.getLong();
        }
    }
}
