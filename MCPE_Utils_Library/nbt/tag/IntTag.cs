using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class IntTag : Tag, INumberTag<int>
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

        public IntTag(string name) : base(name) { }

        public IntTag(string name, int data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write(data);
        }

        protected internal override void load(BinaryReader dis)
        {
            data = dis.ReadInt32();
        }

        public override byte getId()
        {
            return TAG_Int;
        }

        public override string toString()
        {
            return string.Format("IntTag {0} (data: {1})", getName(), data);
        }

        public override Tag Copy()
        {
            return new IntTag(getName(), data);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                IntTag o = obj as IntTag;
                return data == o.data;
            }
            return false;
        }
    }
}
