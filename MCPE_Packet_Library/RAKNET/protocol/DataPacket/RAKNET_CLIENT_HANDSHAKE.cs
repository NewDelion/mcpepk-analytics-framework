using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MCPE_Packet_Library.RAKNET.protocol.DataPacket
{
    public class RAKNET_CLIENT_HANDSHAKE : RAKNET_BASE
    {
        public static byte ID = (byte)0x13;

        public override byte getID()
        {
            return ID;
        }

        public string address;
        public int port;
        public IPEndPoint[] systemAddress = new IPEndPoint[10];

        public long sendPing;
        public long sendPong;

        public override void encode()
        {
            base.encode();
            this.putAddress(new IPEndPoint(IPAddress.Parse(this.address), this.port));
            for (int i = 0; i < 10; i++)
                this.putAddress(this.systemAddress[i]);
            this.putLong(this.sendPing);
            this.putLong(this.sendPong);
        }

        public override void decode()
        {
            base.decode();
            var addr = this.getAddress();
            this.address = addr.Address.ToString();
            this.port = addr.Port;
            for (int i = 0; i < 10; i++)
                this.systemAddress[i] = this.getAddress();
            this.sendPing = this.getLong();
            this.sendPong = this.getLong();
        }
    }
}
