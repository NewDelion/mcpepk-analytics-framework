using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_OPEN_REPLY_2 : RAKNET_BASE
    {
        public static byte ID = (byte)0x08;

        public override byte getID()
        {
            return ID;
        }

        public long serverID;
        public string clientAddress;
        public int clientPort;
        public short mtuSize;

        public override void encode()
        {
            base.encode();
            this.put(RAKNET.MAGIC);
            this.putLong(this.serverID);
            this.putAddress(this.clientAddress, this.clientPort);
            this.putShort(this.mtuSize);
            this.putByte((byte)0x00);
        }

        public override void decode()
        {
            base.decode();
            this.offset += 16;
            this.serverID = this.getLong();
            var address = this.getAddress();
            this.clientAddress = address.AddressFamily.ToString();
            this.clientPort = address.Port;
            this.mtuSize = this.getSignedShort();
        }
    }
}
