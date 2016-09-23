using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol.DataPacket
{
    public class RAKNET_CLIENT_CONNECT : RAKNET_BASE
    {
        public static byte ID = (byte)0x09;

        public override byte getID()
        {
            return ID;
        }

        public long clientID;
        public long sendPing;
        public bool useSecurity = false;

        public override void encode()
        {
            base.encode();
            this.putLong(this.clientID);
            this.putLong(this.sendPing);
            this.putByte((byte)(this.useSecurity ? 1 : 0));
        }

        public override void decode()
        {
            base.decode();
            this.clientID = this.getLong();
            this.sendPing = this.getLong();
            this.useSecurity = this.getByte() > 0;
        }
    }
}
