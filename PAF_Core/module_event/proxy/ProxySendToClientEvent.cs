﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCPE_Packet_Library.RAKNET;

namespace PAF_Core.module_event.proxy
{
    public class ProxySendToClientEvent : ProxySendEvent
    {
        public ProxySendToClientEvent(RAKNET_BASE packet)
        {
            this.packet = packet;
        }
    }
}
