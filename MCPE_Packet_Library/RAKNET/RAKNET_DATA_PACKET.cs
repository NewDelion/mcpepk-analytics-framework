﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET
{
    public class RAKNET_DATA_PACKET : RAKNET_BASE
    {
        public List<object> packets = new List<object>();

        public int seqNumber;

        public byte id = 0;

        public override byte getID()
        {
            return id;
        }

        public override void encode()
        {
            base.encode();
            this.putLTriad(this.seqNumber);
            foreach (var packet in this.packets)
                this.put(packet is RAKNET_ENCAPSULATED_PACKET ? ((RAKNET_ENCAPSULATED_PACKET)packet).toBinary() : (byte[])packet);
        }

        public int length()
        {
            int length = 4;
            foreach (var packet in this.packets)
                length += packet is RAKNET_ENCAPSULATED_PACKET ? ((RAKNET_ENCAPSULATED_PACKET)packet).getTotalLength() : ((byte[])packet).Length;
            return length;
        }

        public override void decode()
        {
            this.id = this.getByte();
            this.seqNumber = this.getLTriad();

            while (!this.feof())
            {
                byte[] data = Binary.subBytes(this.buffer, this.offset);
                RAKNET_ENCAPSULATED_PACKET packet = RAKNET_ENCAPSULATED_PACKET.fromBinary(data, false);
                this.offset += packet.getOffset();
                if (packet.buffer.Length == 0)
                    break;
                this.packets.Add(packet);
            }
        }
    }
}
