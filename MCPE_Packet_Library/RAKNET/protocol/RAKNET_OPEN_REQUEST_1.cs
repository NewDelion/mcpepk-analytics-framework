using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_OPEN_REQUEST_1 : RAKNET_BASE
    {
        public static byte ID = (byte)0x05;

        public override byte getID()
        {
            return ID;
        }

        public byte protocol = RAKNET.PROTOCOL;
        public short mtuSize;

        public override void encode()
        {
            base.encode();
            this.put(RAKNET.MAGIC);
            this.putByte(this.protocol);
            this.put(new byte[this.mtuSize - 18]);
        }

        public override void decode()
        {
            base.decode();
            this.offset += 16;//magic
            this.protocol = this.getByte();
            this.mtuSize = (short)(this.get().Length + 18);
        }
    }
}
