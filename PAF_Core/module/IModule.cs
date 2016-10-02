using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAF_Core;
using PAF_Core.module;
using PAF_Core.module_event;
using PAF_Core.command;

namespace PAF_Core.module
{
    public interface IModule
    {
        string getName();
        string getVersion();
        void initModule(ModuleManager manager, PAFProxy proxy);
        void handleModuleEvent(ModuleEvent ev);
        bool handleCommand(Command command, string[] args);
    }
}
