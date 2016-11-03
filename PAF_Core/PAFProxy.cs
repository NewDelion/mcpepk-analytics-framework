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

        private UdpClient proxy_to_client = null;
        private UdpClient proxy_to_server = null;
        
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

        private int client_port = 19132;
        private int client_unconnected_port = 19132;

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

        private ReaderWriterLock lock_proxy_toclient_port = new ReaderWriterLock();
        private int proxy_toclient_port = 19132;
        public int ProxyToClientPort
        {
            get
            {
                try
                {
                    lock_proxy_toclient_port.AcquireReaderLock(Timeout.Infinite);
                    return proxy_toclient_port;
                }
                finally { lock_proxy_toclient_port.ReleaseReaderLock(); }
            }
            set
            {
                try
                {
                    lock_proxy_toclient_port.AcquireWriterLock(Timeout.Infinite);
                    proxy_toclient_port = value;
                }
                finally { lock_proxy_toclient_port.ReleaseWriterLock(); }
            }
        }

        private ReaderWriterLock lock_proxy_toserver_port = new ReaderWriterLock();
        private int proxy_toserver_port = 19133;
        public int ProxyToServerPort
        {
            get
            {
                try
                {
                    lock_proxy_toserver_port.AcquireReaderLock(Timeout.Infinite);
                    return proxy_toserver_port;
                }
                finally { lock_proxy_toserver_port.ReleaseReaderLock(); }
            }
            set
            {
                try
                {
                    lock_proxy_toserver_port.AcquireWriterLock(Timeout.Infinite);
                    proxy_toserver_port = value;
                }
                finally { lock_proxy_toserver_port.ReleaseWriterLock(); }
            }
        }

        private int current_seqNumber_gap_to_server = 0;
        private int current_seqNumber_gap_to_client = 0;
        private int current_messageIndex_gap_to_server = 0;
        private int current_messageIndex_gap_to_client = 0;

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

            ProxyStarted = true;
            proxy_to_server = new UdpClient(ProxyToServerPort);
            proxy_to_server.Client.ReceiveBufferSize = 1024 * 1024 * 8;
            proxy_to_server.Client.SendBufferSize = 1024 * 1024 * 8;
            proxy_to_server.BeginReceive(ReceiveFromServerCallback, proxy_to_server);
            proxy_to_client = new UdpClient(ProxyToClientPort);
            proxy_to_client.Client.ReceiveBufferSize = 1024 * 1024 * 8;
            proxy_to_client.Client.SendBufferSize = 1024 * 1024 * 8;
            proxy_to_client.BeginReceive(ReceiveFromClientCallback, proxy_to_client);
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

            ProxyStarted = false;
            proxy_to_server.Close();
            proxy_to_client.Close();
        }

        private void ReceiveFromClientCallback(IAsyncResult iar)
        {
            UdpClient socket = (UdpClient)iar.AsyncState;
            IPEndPoint remoteEP = null;
            byte[] recBytes = null;
            try { recBytes = socket.EndReceive(iar, ref remoteEP); }
            catch (SocketException ex)
            {
                Logger.WriteLineWarn("[Error ReciveFromClient]");
                Logger.WriteLineError(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                if (ProxyStarted)
                {
                    Logger.WriteLineWarn("[Error Socket disposed...]");
                    Logger.WriteLineError(ex.Message);
                }
                return;
            }

            if (!remoteEP.Address.Equals(IP_Client) || recBytes.Length == 0)
            {
                proxy_to_client.BeginReceive(ReceiveFromClientCallback, proxy_to_client);
                return;
            }

            var packet = packetManager.CreateInstance_RakNet(recBytes[0]);
            packet.buffer = recBytes;
            packet.decode();
            if (packet is RAKNET_OPEN_REQUEST_2)
            {
                (packet as RAKNET_OPEN_REQUEST_2).serverAddress = Address_Server.Address.ToString();
                (packet as RAKNET_OPEN_REQUEST_2).serverPort = Address_Server.Port;
                (packet as RAKNET_OPEN_REQUEST_2).encode();
                recBytes = packet.buffer;
            }
            else if (packet is RAKNET_UNCONNECTED_PING)
            {
                client_unconnected_port = remoteEP.Port;
            }
            if(packet is RAKNET_DATA_PACKET)
            {
                bool edited = false;
                for (int i = 0; i < (packet as RAKNET_DATA_PACKET).packets.Count;i++)
                {
                    var pk = (packet as RAKNET_DATA_PACKET).packets[i];
                    if (pk is RAKNET_ENCAPSULATED_PACKET && !(pk as RAKNET_ENCAPSULATED_PACKET).hasSplit)
                    {
                        byte[] enpk_buffer = (pk as RAKNET_ENCAPSULATED_PACKET).buffer;
                        if (enpk_buffer != null && enpk_buffer.Length > 0 && packetManager.Exists_RakNet_DataPacket(enpk_buffer[0]))
                        {
                            var dpk = packetManager.CreateInstance_RakNet_DataPacket(enpk_buffer[0]);
                            if (dpk is RAKNET_CLIENT_HANDSHAKE)//Splitに入ってたらアウト…
                            {
                                dpk.buffer = enpk_buffer;
                                (dpk as RAKNET_CLIENT_HANDSHAKE).decode();
                                (dpk as RAKNET_CLIENT_HANDSHAKE).address = Address_Server.Address.ToString();
                                (dpk as RAKNET_CLIENT_HANDSHAKE).port = Address_Server.Port;
                                (dpk as RAKNET_CLIENT_HANDSHAKE).encode();
                                (pk as RAKNET_ENCAPSULATED_PACKET).buffer = dpk.buffer;
                                edited = true;
                            }
                        }
                    }
                }
                if (edited)
                {
                    (packet as RAKNET_DATA_PACKET).encode();
                    recBytes = (packet as RAKNET_DATA_PACKET).buffer;
                }
            }
            var ev = new ProxySendToServerEvent(packet);
            moduleManager.callEvent(ev);
            proxy_to_server.Send(recBytes, recBytes.Length, Address_Server);

            if (socket.Client != null)
                proxy_to_client.BeginReceive(ReceiveFromClientCallback, proxy_to_client);
        }

        private void ReceiveFromServerCallback(IAsyncResult iar)
        {
            UdpClient socket = (UdpClient)iar.AsyncState;
            IPEndPoint remoteEP = null;
            byte[] recBytes = null;
            try { recBytes = socket.EndReceive(iar, ref remoteEP); }
            catch (SocketException ex)
            {
                Logger.WriteLineWarn("[Error ReciveFromServer]");
                Logger.WriteLineError(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                if (ProxyStarted)
                {
                    Logger.WriteLineWarn("[Error Socket disposed...]");
                    Logger.WriteLineError(ex.Message);
                }
                return;
            }

            if (!remoteEP.Equals(Address_Server))
            {
                proxy_to_server.BeginReceive(ReceiveFromServerCallback, proxy_to_server);
                return;
            }

            var packet = packetManager.CreateInstance_RakNet(recBytes[0]);
            packet.buffer = recBytes;
            packet.decode();
            if (packet is RAKNET_DATA_PACKET)
            {
                bool edited = false;
                for (int i = 0; i < (packet as RAKNET_DATA_PACKET).packets.Count; i++)
                {
                    var pk = (packet as RAKNET_DATA_PACKET).packets[i];
                    if (pk is RAKNET_ENCAPSULATED_PACKET && !(pk as RAKNET_ENCAPSULATED_PACKET).hasSplit)
                    {
                        byte[] enpk_buffer = (pk as RAKNET_ENCAPSULATED_PACKET).buffer;
                        if (enpk_buffer != null && enpk_buffer.Length > 0 && packetManager.Exists_RakNet_DataPacket(enpk_buffer[0]))
                        {
                            var dpk = packetManager.CreateInstance_RakNet_DataPacket(enpk_buffer[0]);
                            if (dpk is RAKNET_SERVER_HANDSHAKE)//Splitに入ってたらアウト…
                            {
                                dpk.buffer = enpk_buffer;
                                (dpk as RAKNET_SERVER_HANDSHAKE).decode();
                                (dpk as RAKNET_SERVER_HANDSHAKE).address = IP_Client.ToString();
                                (dpk as RAKNET_SERVER_HANDSHAKE).port = client_port;
                                (dpk as RAKNET_SERVER_HANDSHAKE).encode();
                                (pk as RAKNET_ENCAPSULATED_PACKET).buffer = dpk.buffer;
                                edited = true;
                            }
                        }
                    }
                }
                if (edited)
                {
                    (packet as RAKNET_DATA_PACKET).encode();
                    recBytes = (packet as RAKNET_DATA_PACKET).buffer;
                }
            }
            var ev = new ProxySendToClientEvent(packet);
            moduleManager.callEvent(ev);
            proxy_to_client.Send(recBytes, recBytes.Length, new IPEndPoint(IP_Client, packet is RAKNET_UNCONNECTED_PONG ? client_unconnected_port : client_port));
            
            if (socket.Client != null)
                proxy_to_server.BeginReceive(ReceiveFromServerCallback, proxy_to_server);
        }
    }
}
