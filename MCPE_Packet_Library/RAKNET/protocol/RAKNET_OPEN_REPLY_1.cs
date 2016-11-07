using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_OPEN_REPLY_1 : RAKNET_BASE
    {
        public static byte ID = (byte)0x06;

        public override byte getID()
        {
            return ID;
        }

        public long serverID;
        public short mtuSize;

        public override void encode()
        {
            base.encode();
            this.put(RAKNET.MAGIC);
            this.putLong(this.serverID);
            this.putByte((byte)0x00);
            this.putSignedShort(this.mtuSize);
        }

        public override void decode()
        {
            base.decode();
            this.offset += 16;//magic
            this.serverID = this.getLong();
            this.getByte();
            this.mtuSize = this.getSignedShort();
        }
    }
}
