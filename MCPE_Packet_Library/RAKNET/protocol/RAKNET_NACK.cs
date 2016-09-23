using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol
{
    public class RAKNET_NACK : RAKNET_ACKNOWLEDGE_PACKET
    {
        public static byte ID = (byte)0xa0;

        public override byte getID()
        {
            return ID;
        }
    }
}
