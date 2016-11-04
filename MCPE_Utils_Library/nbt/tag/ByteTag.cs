using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class ByteTag : Tag, INumberTag<int>
    {
        public int data;

        public int getData()
        {
            return data;
        }

        public void setData(int data)
        {
            this.data = data;
        }

        public ByteTag(string name) : base(name) { }

        public ByteTag(string name, int data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write((byte)data);
        }

        protected internal override void load(BinaryReader dis)
        {
            data = dis.ReadByte();
        }

        public override byte getId()
        {
            return TAG_Byte;
        }

        public override string toString()
        {
            return string.Format("ByteTag {0} (data: 0x{1})", this.getName(), ((byte)data).ToString("X2"));
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ByteTag byteTag = obj as ByteTag;
                return data == byteTag.data;
            }
            return false;
        }

        public override Tag Copy()
        {
            return new ByteTag(getName(), data);
        }
    }
}
