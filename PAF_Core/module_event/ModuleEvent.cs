using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core.module_event
{
    public class ModuleEvent
    {
        protected string eventName = null;
        private bool IsCancelled = false;

        public string getEventName()
        {
            return eventName == null ? GetType().Name : eventName;
        }

        public bool isCancelled()
        {
            if (!(this is Cancellable))
                throw new Exception("Event is not Cancellable!!");
            return IsCancelled;
        }

        public void setCancelled()
        {
            setCancelled(true);
        }

        public void setCancelled(bool value)
        {
            if (!(this is Cancellable))
                throw new Exception("Event is not Cancellable!!");
            IsCancelled = value;
        }
    }
}
