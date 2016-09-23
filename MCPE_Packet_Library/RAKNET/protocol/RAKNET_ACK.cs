using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_ACK : RAKNET_ACKNOWLEDGE_PACKET
    {
        public static byte ID = (byte)0xc0;

        public override byte getID()
        {
            return ID;
        }
    }
}
