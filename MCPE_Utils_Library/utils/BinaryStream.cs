using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MCPE_Utils_Library.item;

namespace MCPE_Utils_Library.utils
{
    public class BinaryStream
    {
        public int offset;
        private byte[] buffer = new byte[32];
        private int count;

        public BinaryStream()
        {
            this.buffer = new byte[32];
            this.offset = 0;
            this.count = 0;
        }

        public BinaryStream(byte[] buffer)
        {
            this.buffer = buffer;
            this.offset = 0;
            this.count = buffer.Length;
        }

        public BinaryStream(byte[] buffer, int offset)
        {
            this.buffer = buffer;
            this.offset = offset;
            this.count = buffer.Length;
        }

        public void reset()
        {
            this.buffer = new byte[32];
            this.offset = 0;
            this.count = 0;
        }

        public void setBuffer(byte[] buffer)
        {
            this.buffer = buffer;
            this.count = buffer == null ? -1 : buffer.Length;
        }

        public void setBuffer(byte[] buffer, int offset)
        {
            this.setBuffer(buffer);
            this.setOffset(offset);
        }

        public int getOffset()
        {
            return offset;
        }

        public void setOffset(int offset)
        {
            this.offset = offset;
        }

        public byte[] getBuffer()
        {
            return (byte[])buffer.Clone();
        }

        public int getCount()
        {
            return count;
        }

        public byte[] get()
        {
            return this.get(this.count - this.offset);
        }

        public byte[] get(int len)
        {
            if (len < 0)
            {
                this.offset = this.count - 1;
                return new byte[0];
            }
            len = Math.Min(len, this.getCount() - this.offset);
            this.offset += len;
            return buffer.Skip(offset).Take(len).ToArray();
        }

        public void put(byte[] bytes)
        {
            if (bytes == null)
                return;

            //
        }

        public long getLong()
        {
            return Binary.readLong(this.get(8));
        }

        public void putLong(long l)
        {
            this.put(Binary.writeLong(l));
        }

        public int getInt()
        {
            return Binary.readInt(this.get(4));
        }

        public void putInt(int i)
        {
            this.put(Binary.writeInt(i));
        }

        public long getLLong()
        {
            return Binary.readLLong(this.get(8));
        }

        public void putLLong(long l)
        {
            this.put(Binary.writeLLong(l));
        }

        public int getLInt()
        {
            return Binary.readLInt(this.get(4));
        }

        public int getShort()
        {
            return Binary.readShort(this.get(2));
        }

        public void putShort(short s)
        {
            this.put(Binary.writeShort(s));
        }

        public short getSignedShort()
        {
            return Binary.readSignedShort(this.get(2));
        }

        public void putSignedShort(short s)
        {
            this.put(Binary.writeShort(s));
        }

        public int getLShort()
        {
            return Binary.readLShort(this.get(2));
        }

        public void putLShort(short s)
        {
            this.put(Binary.writeLShort(s));
        }

        public short getSignedLShort()
        {
            return Binary.readSignedLShort(this.get(2));
        }

        public void getSigndLShort(short s)
        {
            this.put(Binary.writeLShort(s));
        }

        public float getFloat()
        {
            return Binary.readFloat(this.get(4));
        }

        public void putFloat(float v)
        {
            this.put(Binary.writeFloat(v));
        }

        public float getLFloat()
        {
            return Binary.readLFloat(this.get(4));
        }

        public void putLFloat(float v)
        {
            this.put(Binary.writeLFloat(v));
        }

        public int getTriad()
        {
            return Binary.readTriad(this.get(3));
        }

        public void putTriad(int triad)
        {
            this.put(Binary.writeTriad(triad));
        }

        public int getLTriad()
        {
            return Binary.readLTriad(this.get(3));
        }

        public void putLTriad(int triad)
        {
            this.put(Binary.writeLTriad(triad));
        }

        public byte getSignedByte()
        {
            return this.buffer[this.offset++];
        }

        public bool getBoolean()
        {
            return this.getByte() == 0x01;
        }

        public void putBoolean(bool b)
        {
            this.putByte((byte)(b ? 0x01 : 0x00));
        }

        public int getByte()
        {
            return this.buffer[this.offset++] & 0xff;
        }

        public void putByte(byte b)
        {
            this.put(new byte[] { b });
        }

        public byte[][] getDataArray()
        {
            return this.getDataArray(10);
        }

        public byte[][] getDataArray(int len)
        {
            byte[][] data = new byte[len][];
            for (int i = 0; i < len && !this.feof(); ++i)
                data[i] = this.get(this.getLTriad());
            return data;
        }

        public void putDataArray(byte[][] data)
        {
            foreach (byte[] v in data)
            {
                this.putTriad(v.Length);
                this.put(v);
            }
        }

        public void putUUID(Guid uuid)
        {
            this.put(Binary.writeUUID(uuid));
        }

        public Guid getUUID()
        {
            return Binary.readUUID(this.get(16));
        }

        public void putSkin(MCPE_Utils_Library.entity.data.Skin skin)
        {
            this.putString(skin.getModel());
            this.putUnsignedVarInt(Convert.ToUInt32(skin.getData().Length));
            this.put(skin.getData());
        }

        public MCPE_Utils_Library.entity.data.Skin getSkin()
        {
            string modelId = this.getString();
            byte[] skinData = this.get((int)this.getUnsignedVarInt());
            return new MCPE_Utils_Library.entity.data.Skin(skinData, modelId);
        }

        public Item getSlot()
        {
            int id = this.getVarInt();

            if (id <= 0)
                return MCPE_Utils_Library.item.Item.get(0, 0, 0);
            int auxValue = this.getVarInt();
            int data = auxValue >> 8;
            int cnt = auxValue & 0xff;

            int nbtLen = this.getLShort();
            byte[] nbt = new byte[0];
            if (nbtLen > 0)
                nbt = this.get(nbtLen);

            return Item.get(id, data, cnt, nbt);
        }

        public void putSlot(Item item)
        {
            if (item == null || item.getId() == 0)
            {
                this.putVarInt(0);
                return;
            }

            this.putVarInt(item.getId());
            int auxValue = ((item.HasMeta() ? item.getDamage() : -1) << 8);
            this.putVarInt(auxValue);
            byte[] nbt = item.getCompoundTag();
            this.putLShort((short)nbt.Length);
            this.put(nbt);
        }

        public string getString()
        {
            return Encoding.UTF8.GetString(this.get((int)this.getUnsignedVarInt()));
        }

        public void putString(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            this.putUnsignedVarInt(Convert.ToUInt32(b.Length));
            this.put(b);
        }

        public uint getUnsignedVarInt()
        {
            return VarInt.ReadUInt32(this);
        }

        public void putUnsignedVarInt(uint v)
        {
            VarInt.WriteUInt32(this, v);
        }

        public int getVarInt()
        {
            return VarInt.ReadInt32(this);
        }

        public void putVarInt(int v)
        {
            VarInt.WriteInt32(this, v);
        }

        public ulong getUnsignedVarLong()
        {
            return VarInt.ReadUInt64(this);
        }

        public void putUnsignedVarLong(ulong v)
        {
            VarInt.WriteUInt64(this, v);
        }

        public long getVarLong()
        {
            return VarInt.ReadInt64(this);
        }

        public void putVarLong(long v)
        {
            VarInt.WriteInt64(this, v);
        }

        public long getEntityId()
        {
            return this.getVarLong();
        }

        public void putEntityId(long v)
        {
            this.putVarLong(v);
        }



        public bool feof()
        {
            return this.offset < 0 || this.offset >= this.buffer.Length;
        }
    }
}
