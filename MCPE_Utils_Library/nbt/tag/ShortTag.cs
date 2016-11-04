using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class ShortTag : Tag, INumberTag<int>
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

        public ShortTag(string name) : base(name) { }

        public ShortTag(string name, int data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write((short)data);
        }

        protected internal override void load(BinaryReader dis)
        {
            data = dis.ReadUInt16();
        }

        public override byte getId()
        {
            return TAG_Short;
        }

        public override string toString()
        {
            return "" + data;
        }

        public override Tag Copy()
        {
            return new ShortTag(getName(), data);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ShortTag o = obj as ShortTag;
                return data == o.data;
            }
            return false;
        }
    }
}
