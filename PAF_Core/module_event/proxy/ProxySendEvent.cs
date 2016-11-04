using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core.module_event.proxy
{
    public class ProxySendEvent : ModuleEvent
    {
        public MCPE_Packet_Library.RAKNET.RAKNET_BASE packet = null;
    }
}
