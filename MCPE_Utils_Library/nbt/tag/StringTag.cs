using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class StringTag : Tag
    {
        public string data;

        public StringTag(string name) : base(name) { }

        public StringTag(string name, string data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write(data);
        }

        protected internal override void load(BinaryReader dis)
        {
            data = dis.ReadString();
        }

        public override byte getId()
        {
            return TAG_String;
        }

        public override string toString()
        {
            return string.Format("StringTag {0} (data: {1})", getName(), data);
        }

        public override Tag Copy()
        {
            return new StringTag(getName(), data);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                StringTag o = obj as StringTag;
                return ((data == null && o.data == null) || (data != null && data.Equals(o.data)));
            }
            return false;
        }
    }
}
