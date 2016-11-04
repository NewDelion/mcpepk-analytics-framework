using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class LongTag : Tag, INumberTag<long>
    {
        public long data;

        public long getData()
        {
            return data;
        }

        public void setData(long data)
        {
            this.data = data;
        }

        public LongTag(string name) : base(name) { }

        public LongTag(string name, long data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write(data);
        }

        protected internal override void load(BinaryReader dis)
        {
            data = dis.ReadInt64();
        }

        public override byte getId()
        {
            return TAG_Long;
        }

        public override string toString()
        {
            return string.Format("LongTag {0} (data: {1})", getName(), data);
        }

        public override Tag Copy()
        {
            return new LongTag(getName(), data);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                LongTag o = obj as LongTag;
                return data == o.data;
            }
            return false;
        }
    }
}
