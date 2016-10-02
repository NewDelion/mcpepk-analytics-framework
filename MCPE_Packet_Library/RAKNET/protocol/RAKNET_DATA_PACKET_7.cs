using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_DATA_PACKET_7 : RAKNET_DATA_PACKET
    {
        public static byte ID = (byte)0x87;

        public override byte getID()
        {
            return ID;
        }
    }
}
