using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MCPE_Packet_Library.RAKNET
{
    public abstract class RAKNET_BASE
    {
        protected int offset = 0;
        public byte[] buffer;
        public long sendTime;

        public abstract byte getID();

        protected byte[] get(int len)
        {
            if (len < 0)
            {
                this.offset = this.buffer.Length - 1;
                return new byte[0];
            }

            byte[] buffer = new byte[len];
            for (int i = 0; i < len; i++)
                buffer[i] = this.buffer[this.offset++];
            return buffer;
        }

        protected byte[] getAll()
        {
            return this.get();
        }

        protected byte[] get()
        {
            return this.buffer.Skip(this.offset).ToArray();
        }

        protected long getLong()
        {
            return Binary.readLong(this.get(8));
        }

        protected int getInt()
        {
            return Binary.readInt(this.get(4));
        }

        protected int getShort()
        {
            return Binary.readShort(this.get(2));
        }

        protected short getSignedShort()
        {
            return (short)this.getShort();
        }

        protected int getTriad()
        {
            return Binary.readTriad(this.get(3));
        }

        protected int getLTriad()
        {
            return Binary.readLTriad(this.get(3));
        }

        protected byte getByte()
        {
            return this.buffer[this.offset++];
        }

        protected string getString()
        {
            return Encoding.UTF8.GetString(this.get(this.getSignedShort()));
        }

        protected IPEndPoint getAddress()
        {
            byte version = this.getByte();
            if(version == 4)
            {
                string addr = ((~this.getByte()) & 0xff) + "." + ((~this.getByte()) & 0xff) + "." + ((~this.getByte()) & 0xff) + "." + ((~this.getByte()) & 0xff);
                int port = this.getShort();
                return new IPEndPoint(IPAddress.Parse(addr), port);
            }
            else
            {
                //todo IPV6 SUPPORT
                return null;
            }
        }

        protected bool feof()
        {
            return !(this.offset >= 0 && this.offset + 1 <= this.buffer.Length);
        }
        
        protected void put(byte[] b)
        {
            this.buffer = Binary.appendBytes(this.buffer, b);
        }

        protected void putLong(long v)
        {
            this.put(Binary.writeLong(v));
        }

        protected void putInt(int v)
        {
            this.put(Binary.writeInt(v));
        }

        protected void putShort(int v)
        {
            this.put(Binary.writeShort(v));
        }

        protected void putSignedShort(short v)
        {
            this.put(Binary.writeShort(v & 0xffff));
        }

        protected void putTriad(int v)
        {
            this.put(Binary.writeTriad(v));
        }

        protected void putLTriad(int v)
        {
            this.put(Binary.writeLTriad(v));
        }

        protected void putByte(byte b)
        {
            this.buffer = this.buffer.Concat(new byte[] { b }).ToArray();
        }

        protected void putString(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            this.putShort(b.Length);
            this.put(b);
        }

        protected void putAddress(string addr, int port, byte version)
        {
            this.putByte(version);
            if(version == 0x04)
            {
                foreach (int b in addr.Split('.').Select(d => int.Parse(d)))
                {
                    this.putByte((byte)((~b) & 0xff));
                }
                this.putShort(port);
            }
            else
            {
                //todo ipv6
            }
        }

        protected void putAddress(string addr, int port)
        {
            this.putAddress(addr, port, (byte)0x04);
        }

        protected void putAddress(IPEndPoint address)
        {
            this.putAddress(address.Address.ToString(), address.Port);
        }

        public virtual void encode()
        {
            this.buffer = new byte[] { getID() };
        }

        public virtual void decode()
        {
            this.offset = 1;
        }
    }
}
