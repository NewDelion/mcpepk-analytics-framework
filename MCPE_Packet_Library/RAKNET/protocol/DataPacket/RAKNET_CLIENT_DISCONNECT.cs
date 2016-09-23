using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET.protocol.DataPacket
{
    public class RAKNET_CLIENT_DISCONNECT : RAKNET_BASE
    {
        public static byte ID = (byte)0x15;

        public override byte getID()
        {
            return ID;
        }
    }
}
