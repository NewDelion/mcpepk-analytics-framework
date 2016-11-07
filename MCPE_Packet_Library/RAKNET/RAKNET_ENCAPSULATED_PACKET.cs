using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET
{
    public class RAKNET_ENCAPSULATED_PACKET
    {
        public int reliability;
        public bool hasSplit = false;
        public int length = 0;
        public int? messageIndex = null;
        public int? orderIndex = null;
        public int? orderChannel = null;
        public int? splitCount = null;
        public int? splitID = null;
        public int? splitIndex = null;
        public byte[] buffer;
        public bool needACK = false;
        public int? identifierACK = null;

        private int offset;

        public int getOffset()
        {
            return offset;
        }

        public static RAKNET_ENCAPSULATED_PACKET fromBinary(byte[] binary)
        {
            return fromBinary(binary, false);
        }

        public static RAKNET_ENCAPSULATED_PACKET  fromBinary(byte[] binary, bool @internal)
        {
            RAKNET_ENCAPSULATED_PACKET packet = new RAKNET_ENCAPSULATED_PACKET();

            int flags = binary[0] & 0xff;

            packet.reliability = ((flags & 0xe0) >> 5);
            packet.hasSplit = ((flags & 0x10)) > 0;
            int length, offset = 0;
            if (@internal)
            {
                length = Binary.readInt(Binary.subBytes(binary, 1, 4));
                packet.identifierACK = Binary.readInt(Binary.subBytes(binary, 5, 4));
                offset = 9;
            }
            else
            {
                length = (int)Math.Ceiling(((double)Binary.readShort(Binary.subBytes(binary, 1, 2)) / 8));
                offset = 3;
                packet.identifierACK = null;
            }

            if(packet.reliability > 0)
            {
                if(packet.reliability >=2&&packet.reliability != 5)
                {
                    packet.messageIndex = Binary.readLTriad(Binary.subBytes(binary, offset, 3));
                    offset += 3;
                }
                if (packet.reliability <= 4 && packet.reliability != 2)
                {
                    packet.orderIndex = Binary.readLTriad(Binary.subBytes(binary, offset, 3));
                    offset += 3;
                    packet.orderChannel = binary[offset++] & 0xff;
                }
            }

            if (packet.hasSplit)
            {
                packet.splitCount = Binary.readInt(Binary.subBytes(binary, offset, 4));
                offset += 4;
                packet.splitID = Binary.readShort(Binary.subBytes(binary, offset, 2));
                offset += 2;
                packet.splitIndex = Binary.readInt(Binary.subBytes(binary, offset, 4));
                offset += 4;
            }

            packet.buffer = Binary.subBytes(binary, offset, length);
            offset += length;
            packet.offset = offset;

            return packet;
        }

        public int getTotalLength()
        {
            return 3 + this.buffer.Length + (this.messageIndex != null ? 3 : 0) + (this.orderIndex != null ? 4 : 0) + (this.hasSplit ? 10 : 0);
        }

        public byte[] toBinary()
        {
            return toBinary(false);
        }

        public byte[] toBinary(bool @internal)
        {
            using(System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                stream.WriteByte((byte)((reliability << 5) | (hasSplit ? 16 : 0)));
                if (@internal)
                {
                    stream.Write(Binary.writeInt(buffer.Length), 0, 4);
                    stream.Write(Binary.writeInt(identifierACK == null ? 0 : (int)identifierACK), 0, 4);
                }
                else
                {
                    stream.Write(Binary.writeShort((short)(buffer.Length << 3)), 0, 2);
                }

                if(reliability > 0)
                {
                    if (reliability >= 2 && reliability != 5)
                        stream.Write(Binary.writeLTriad(messageIndex == null ? 0 : (int)messageIndex), 0, 3);
                    if (reliability <= 4 && reliability != 2)
                    {
                        stream.Write(Binary.writeLTriad((int)orderChannel), 0, 3);
                        stream.WriteByte((byte)(orderChannel & 0xff));
                    }
                }

                if (hasSplit)
                {
                    stream.Write(Binary.writeInt((int)splitCount), 0, 4);
                    stream.Write(Binary.writeShort((short)splitID), 0, 2);
                    stream.Write(Binary.writeInt((int)splitIndex), 0, 4);
                }

                stream.Write(buffer, 0, buffer.Length);

                return stream.ToArray();
            }
        }

        public string toString()
        {
            return Binary.bytesToHexString(this.toBinary());
        }
    }
}
