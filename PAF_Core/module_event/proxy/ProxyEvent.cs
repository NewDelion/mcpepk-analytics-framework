using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core.module_event.proxy
{
    public class ProxyStartEvent : ModuleEvent, Cancellable { }
    public class ProxyStopEvent : ModuleEvent, Cancellable { }
}
