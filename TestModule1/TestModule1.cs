using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAF_Core;
using PAF_Core.command;
using PAF_Core.module;
using PAF_Core.module_event;

namespace TestModule1
{
    public class TestModule1 : PAF_Core.module.IModule
    {
        private PAFProxy proxy = null;
        private ModuleManager manager = null;

        public string getName()
        {
            return "TestModule1";
        }

        public string getVersion()
        {
            return "0.1.0";
        }

        public bool handleCommand(Command command, string[] args)
        {
            if (command.getName().Equals("test1"))
                Logger.WriteLineInfo("TestModule1のコマンドだよー！");
            return false;
        }

        public void handleModuleEvent(ModuleEvent ev)
        {
            
        }

        public void initModule(ModuleManager manager, PAFProxy proxy)
        {
            this.manager = manager;
            this.proxy = proxy;

            proxy.command_map.register(new Command("test1", "TestModule1のコマンド", "test1 <へむへむ>"), this);
        }
    }
}
