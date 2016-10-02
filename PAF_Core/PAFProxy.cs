using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using PAF_Core;
using PAF_Core.module;
using PAF_Core.packet;
using PAF_Core.command;
using PAF_Core.module_event;
using PAF_Core.module_event.proxy;
using MCPE_Packet_Library;
using MCPE_Packet_Library.RAKNET;
using MCPE_Packet_Library.RAKNET.protocol;
using MCPE_Packet_Library.RAKNET.protocol.DataPacket;

namespace PAF_Core
{
    public class PAFProxy
    {
        public static PAFProxy instance = null;
        
        public CommandMap command_map = null;
        public ModuleManager moduleManager = null;
        public PacketManager packetManager = null;

        public DefaultCommandHandler defaultCommandHandler = new DefaultCommandHandler();
        
        private ReaderWriterLock lock_ProxyStarted = new ReaderWriterLock();
        private bool _ProxyStarted = false;
        public bool ProxyStarted
        {
            get
            {
                try
                {
                    lock_ProxyStarted.AcquireReaderLock(Timeout.Infinite);
                    return _ProxyStarted;
                }
                finally
                {
                    lock_ProxyStarted.ReleaseReaderLock();
                }
            }
            private set
            {
                try
                {
                    lock_ProxyStarted.AcquireWriterLock(Timeout.Infinite);
                    _ProxyStarted = value;
                }
                finally
                {
                    lock_ProxyStarted.ReleaseWriterLock();
                }
            }
        }

        private ReaderWriterLock lock_ip_client = new ReaderWriterLock();
        private IPAddress ip_client = null;
        public IPAddress IP_Client
        {
            get
            {
                try
                {
                    lock_ip_client.AcquireReaderLock(Timeout.Infinite);
                    return ip_client;
                }
                finally { lock_ip_client.ReleaseReaderLock(); }
            }
            set
            {
                try
                {
                    lock_ip_client.AcquireWriterLock(Timeout.Infinite);
                    ip_client = value;
                }
                finally { lock_ip_client.ReleaseWriterLock(); }
            }
        }

        private ReaderWriterLock lock_address_server = new ReaderWriterLock();
        private IPEndPoint address_server = null;
        public IPEndPoint Address_Server
        {
            get
            {
                try
                {
                    lock_address_server.AcquireReaderLock(Timeout.Infinite);
                    return address_server;
                }
                finally { lock_address_server.ReleaseReaderLock(); }
            }
            set
            {
                try
                {
                    lock_address_server.AcquireWriterLock(Timeout.Infinite);
                    address_server = value;
                }
                finally { lock_address_server.ReleaseWriterLock(); }
            }
        }

        private ReaderWriterLock lock_proxy_port = new ReaderWriterLock();
        private int proxy_port = 19133;
        public int ProxyPort
        {
            get
            {
                try
                {
                    lock_proxy_port.AcquireReaderLock(Timeout.Infinite);
                    return proxy_port;
                }
                finally { lock_proxy_port.ReleaseReaderLock(); }
            }
            set
            {
                try
                {
                    lock_proxy_port.AcquireWriterLock(Timeout.Infinite);
                    proxy_port = value;
                }
                finally { lock_proxy_port.ReleaseWriterLock(); }
            }
        }
        
        public PAFProxy()
        {
            this.command_map = new CommandMap();
            this.init_register_command();

            this.packetManager = new PacketManager();
            this.init_register_packet();

            instance = this;

            this.moduleManager = new ModuleManager();
            this.moduleManager.LoadModules();
        }


        private void init_register_command()
        {
            this.defaultCommandHandler.initModule(moduleManager, this);
        }
        private void init_register_packet()
        {
            this.packetManager.Register_RakNet(typeof(RAKNET_UNCONNECTED_PING));
            this.packetManager.Register_RakNet(typeof(RAKNET_UNCONNECTED_PING_OPEN_CONNECTIONS));
            this.packetManager.Register_RakNet(typeof(RAKNET_UNCONNECTED_PONG));
            this.packetManager.Register_RakNet(typeof(RAKNET_ACK));
            this.packetManager.Register_RakNet(typeof(RAKNET_NACK));
            this.packetManager.Register_RakNet(typeof(RAKNET_OPEN_REQUEST_1));
            this.packetManager.Register_RakNet(typeof(RAKNET_OPEN_REPLY_1));
            this.packetManager.Register_RakNet(typeof(RAKNET_OPEN_REQUEST_2));
            this.packetManager.Register_RakNet(typeof(RAKNET_OPEN_REPLY_2));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_0));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_1));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_2));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_3));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_4));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_5));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_6));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_7));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_8));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_9));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_A));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_B));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_C));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_D));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_E));
            this.packetManager.Register_RakNet(typeof(RAKNET_DATA_PACKET_F));

            this.packetManager.Register_RakNet_DataPackt(typeof(RAKNET_CLIENT_CONNECT));
            this.packetManager.Register_RakNet_DataPackt(typeof(RAKNET_CLIENT_DISCONNECT));
            this.packetManager.Register_RakNet_DataPackt(typeof(RAKNET_CLIENT_HANDSHAKE));
            this.packetManager.Register_RakNet_DataPackt(typeof(RAKNET_PING));
            this.packetManager.Register_RakNet_DataPackt(typeof(RAKNET_PONG));
            this.packetManager.Register_RakNet_DataPackt(typeof(RAKNET_SERVER_HANDSHAKE));
        }

        public void Start()
        {
            if(Address_Server == null)
            {
                Logger.WriteLineError("サーバのアドレスが設定されていません。");
                Logger.WriteLineWarn("プロキシを起動できませんでした。");
                return;
            }

            if(IP_Client == null)
            {
                Logger.WriteLineError("クライアントのIPが設定されていません。");
                Logger.WriteLineWarn("プロキシを起動できませんでした。");
                return;
            }

            if (ProxyStarted)
            {
                Logger.WriteLineWarn("プロキシは既に起動しています。");
                return;
            }

            var ev = new ProxyStartEvent();
            moduleManager.callEvent(ev);
            if (ev.isCancelled())
                return;

            //起動処理
        }

        public void Stop()
        {
            if (!ProxyStarted)
            {
                Logger.WriteLineWarn("プロキシは既に停止しています。");
                return;
            }

            var ev = new ProxyStopEvent();
            moduleManager.callEvent(ev);
            if (ev.isCancelled())
                return;

            //停止処理
        }
    }
}
