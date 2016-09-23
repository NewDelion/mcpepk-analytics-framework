using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_UNCONNECTED_PING : RAKNET_BASE
    {
        public static byte ID = (byte)0x01;

        public override byte getID()
        {
            return ID;
        }

        public long pingID;

        public override void encode()
        {
            base.encode();
            this.putLong(this.pingID);
            this.put(RAKNET.MAGIC);
        }

        public override void decode()
        {
            base.decode();
            this.pingID = this.getLong();
        }
    }
}
