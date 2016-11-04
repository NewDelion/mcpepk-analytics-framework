using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCPE_Utils_Library.nbt.tag
{
    public class DoubleTag : Tag, INumberTag<double>
    {
        public double data;

        public double getData()
        {
            return data;
        }

        public void setData(double data)
        {
            this.data = data;
        }

        public DoubleTag(string name) : base(name) { }

        public DoubleTag(string name, double data) : base(name)
        {
            this.data = data;
        }

        protected internal override void write(BinaryWriter dos)
        {
            dos.Write(data);
        }

        protected internal override void load(BinaryReader dis)
        {
            data = dis.ReadSingle();
        }

        public override byte getId()
        {
            return TAG_Double;
        }

        public override string toString()
        {
            return string.Format("DoubleTag {0} (data: {1})", getName(), data);
        }

        public override Tag Copy()
        {
            return new DoubleTag(getName(), data);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                DoubleTag o = obj as DoubleTag;
                return data == o.data;
            }
            return false;
        }
    }
}
