using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCPE_Packet_Library;
using MCPE_Packet_Library.RAKNET;
using MCPE_Packet_Library.RAKNET.protocol;
using MCPE_Packet_Library.RAKNET.protocol.DataPacket;
using System.Threading;

namespace PAF_Core.packet
{
    public class PacketManager
    {
        private ReaderWriterLock lock_RakNetPacketPool = new ReaderWriterLock();
        private List<Type> RakNetPacketPool = new List<Type>();

        private ReaderWriterLock lock_RakNetDataPacketPool = new ReaderWriterLock();
        private List<Type> RakNetDataPacketPool = new List<Type>();

        public PacketManager()
        {
            
        }

        public bool Register_RakNet(Type packet)
        {
            if (!packet.IsSubclassOf(typeof(RAKNET_BASE)))
                return false;
            try
            {
                lock_RakNetPacketPool.AcquireWriterLock(Timeout.Infinite);
                RakNetPacketPool.Add(packet);
            }
            finally
            {
                lock_RakNetPacketPool.ReleaseWriterLock();
            }
            return true;
        }
        public bool Exists_RakNet(byte id)
        {
            try
            {
                lock_RakNetPacketPool.AcquireReaderLock(Timeout.Infinite);
                return RakNetPacketPool.Exists(d => ((RAKNET_BASE)Activator.CreateInstance(d)).getID() == id);
            }
            finally
            {
                lock_RakNetPacketPool.ReleaseReaderLock();
            }
        }
        public RAKNET_BASE CreateInstance_RakNet(byte id)
        {
            try
            {
                lock_RakNetPacketPool.AcquireReaderLock(Timeout.Infinite);
                RAKNET_BASE tmp = null;
                return RakNetPacketPool.Exists(d => (tmp = (RAKNET_BASE)Activator.CreateInstance(d)).getID() == id) ? tmp : null;
            }
            finally
            {
                lock_RakNetPacketPool.ReleaseReaderLock();
            }
        }

        public bool Register_RakNet_DataPackt(Type packet)
        {
            if (!packet.IsSubclassOf(typeof(RAKNET_BASE)))
                return false;
            try
            {
                lock_RakNetDataPacketPool.AcquireWriterLock(Timeout.Infinite);
                RakNetDataPacketPool.Add(packet);
            }
            finally
            {
                lock_RakNetDataPacketPool.ReleaseWriterLock();
            }
            return true;
        }
        public bool Exists_RakNet_DataPacket(byte id)
        {
            try
            {
                lock_RakNetDataPacketPool.AcquireReaderLock(Timeout.Infinite);
                return RakNetDataPacketPool.Exists(d => ((RAKNET_BASE)Activator.CreateInstance(d)).getID() == id);
            }
            finally
            {
                lock_RakNetDataPacketPool.ReleaseReaderLock();
            }
        }
        public RAKNET_BASE CreateInstance_RakNet_DataPacket(byte id)
        {
            try
            {
                lock_RakNetDataPacketPool.AcquireReaderLock(Timeout.Infinite);
                RAKNET_BASE tmp = null;
                return RakNetDataPacketPool.Exists(d => (tmp = (RAKNET_BASE)Activator.CreateInstance(d)).getID() == id) ? tmp : null;
            }
            finally
            {
                lock_RakNetDataPacketPool.ReleaseReaderLock();
            }
        }
    }
}
