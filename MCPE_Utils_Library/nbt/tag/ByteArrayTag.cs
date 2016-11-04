using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class ByteArrayTag : Tag
    {
        public byte[] data;

        public ByteArrayTag(string name) : base(name) { }

        public ByteArrayTag(string name, byte[] data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            if (data == null)
            {
                dos.Write(0);
                return;
            }
            dos.Write(data.Length);
            dos.Write(data);
        }

        protected internal override void load(BinaryReader dis)
        {
            int length = dis.ReadInt32();
            data = dis.ReadBytes(length);
        }

        public override byte getId()
        {
            return TAG_Byte_Array;
        }

        public override string toString()
        {
            return string.Format("ByteArrayTag {0} (data: 0x{1} [{2} bytes])", getName(), string.Join("", data.Select(d => d.ToString("X2")), data.Length));
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ByteArrayTag byteArrayTag = obj as ByteArrayTag;
                return ((data == null && byteArrayTag.data == null) || (data != null && Array.Equals(data, byteArrayTag.data)));
            }
            return false;
        }

        public override Tag Copy()
        {
            return new ByteArrayTag(getName(), (byte[])data.Clone());
        }
    }
}
