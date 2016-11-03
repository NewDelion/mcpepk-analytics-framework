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
            if(ev is PAF_Core.module_event.proxy.ProxyStartEvent)
            {
                Logger.WriteLineInfo("プロキシが起動するようですね");
            }
            else if (ev is PAF_Core.module_event.proxy.ProxyStopEvent)
            {
                Logger.WriteLineInfo("プロキシが停止したようですね");
            }
            else if(ev is PAF_Core.module_event.proxy.ProxySendToServerEvent)
            {
                //Logger.WriteLineInfo(string.Format("SendToServer: {0}", (ev as PAF_Core.module_event.proxy.ProxySendToServerEvent).packet.GetType().Name));
                if((ev as PAF_Core.module_event.proxy.ProxySendToServerEvent).packet is MCPE_Packet_Library.RAKNET.RAKNET_DATA_PACKET)
                {
                    foreach(var pk in ((ev as PAF_Core.module_event.proxy.ProxySendToServerEvent).packet as MCPE_Packet_Library.RAKNET.RAKNET_DATA_PACKET).packets.Where(d=>d is MCPE_Packet_Library.RAKNET.RAKNET_ENCAPSULATED_PACKET && !(d as MCPE_Packet_Library.RAKNET.RAKNET_ENCAPSULATED_PACKET).hasSplit).Cast<MCPE_Packet_Library.RAKNET.RAKNET_ENCAPSULATED_PACKET>())
                    {
                        if (pk.buffer[0] == 0xfe)
                            Logger.WriteLineInfo(string.Format("ID(To Server MCPE): 0x{0}", pk.buffer[1].ToString("X2")));
                        else
                            Logger.WriteLineInfo(string.Format("ID(To Server): 0x{0}", pk.buffer[0].ToString("X2")));
                    }
                    //Logger.WriteLineInfo(string.Format("seqNumber: {0}", ((ev as PAF_Core.module_event.proxy.ProxySendToServerEvent).packet as MCPE_Packet_Library.RAKNET.RAKNET_DATA_PACKET).seqNumber));
                }
            }
            else if(ev is PAF_Core.module_event.proxy.ProxySendToClientEvent)
            {
                //Logger.WriteLineInfo(string.Format("SendToClient: {0}", (ev as PAF_Core.module_event.proxy.ProxySendToClientEvent).packet.GetType().Name));
                if ((ev as PAF_Core.module_event.proxy.ProxySendToClientEvent).packet is MCPE_Packet_Library.RAKNET.RAKNET_DATA_PACKET)
                {
                    foreach (var pk in ((ev as PAF_Core.module_event.proxy.ProxySendToClientEvent).packet as MCPE_Packet_Library.RAKNET.RAKNET_DATA_PACKET).packets.Where(d => d is MCPE_Packet_Library.RAKNET.RAKNET_ENCAPSULATED_PACKET && !(d as MCPE_Packet_Library.RAKNET.RAKNET_ENCAPSULATED_PACKET).hasSplit).Cast<MCPE_Packet_Library.RAKNET.RAKNET_ENCAPSULATED_PACKET>())
                    {
                        if (pk.buffer[0] == 0xfe)
                            Logger.WriteLineInfo(string.Format("ID(To Client MCPE): 0x{0}", pk.buffer[1].ToString("X2")));
                        else
                            Logger.WriteLineInfo(string.Format("ID(To Client): 0x{0}", pk.buffer[0].ToString("X2")));
                    }
                    //Logger.WriteLineInfo(string.Format("seqNumber: {0}", ((ev as PAF_Core.module_event.proxy.ProxySendToClientEvent).packet as MCPE_Packet_Library.RAKNET.RAKNET_DATA_PACKET).seqNumber));
                }
            }
        }

        public void initModule(ModuleManager manager, PAFProxy proxy)
        {
            this.manager = manager;
            this.proxy = proxy;

            proxy.command_map.register(new Command("test1", "TestModule1のコマンド", "test1 <へむへむ>"), this);
        }
    }
}
