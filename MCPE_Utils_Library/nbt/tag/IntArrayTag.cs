using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class IntArrayTag : Tag
    {
        public int[] data;

        public IntArrayTag(string name) : base(name) { }

        public IntArrayTag(string name, int[] data): base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write(data.Length);
            foreach (int aData in data)
                dos.Write(aData);
        }

        protected internal override void load(BinaryReader dis)
        {
            int length = dis.ReadInt32();
            data = new int[length];
            for (int i = 0; i < length; i++)
                data[i] = dis.ReadInt32();
        }

        public override byte getId()
        {
            return TAG_Int_Array;
        }

        public override string toString()
        {
            return string.Format("IntArrayTag {0} [ {1} bytes]", getName(), data.Length);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                IntArrayTag intArrayTag = obj as IntArrayTag;
                return ((data == null && intArrayTag.data == null) || (data != null && Array.Equals(data, intArrayTag.data)));
            }
            return false;
        }

        public override Tag Copy()
        {
            return new IntArrayTag(getName(), (int[])data.Clone());
        }
    }
}
