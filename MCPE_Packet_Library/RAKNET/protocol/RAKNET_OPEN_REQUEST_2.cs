using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_OPEN_REQUEST_2 : RAKNET_BASE
    {
        public static byte ID = (byte)0x07;

        public override byte getID()
        {
            return ID;
        }

        public long clientID;
        public string serverAddress;
        public int serverPort;
        public short mtuSize;

        public override void encode()
        {
            base.encode();
            this.put(RAKNET.MAGIC);
            this.putAddress(this.serverAddress, (short)this.serverPort);
            this.putSignedShort(this.mtuSize);
            this.putLong(this.clientID);
        }

        public override void decode()
        {
            base.decode();
            this.offset += 16;//magic
            var address = this.getAddress();
            this.serverAddress = address.Address.ToString();
            this.serverPort = address.Port;
            this.mtuSize = this.getSignedShort();
            this.clientID = this.getLong();
        }
    }
}
