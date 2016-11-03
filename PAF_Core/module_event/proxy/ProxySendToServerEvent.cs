using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCPE_Packet_Library.RAKNET;

namespace PAF_Core.module_event.proxy
{
    public class ProxySendToServerEvent : ModuleEvent
    {
        public RAKNET_BASE packet = null;

        public ProxySendToServerEvent(RAKNET_BASE packet)
        {
            this.packet = packet;
        }
    }
}
