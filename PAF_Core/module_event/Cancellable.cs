using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core.module_event
{
    interface Cancellable
    {
        bool isCancelled();
        void setCancelled();
        void setCancelled(bool forceCancel);
    }
}
