using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class FloatTag : Tag, INumberTag<float>
    {
        public float data;

        public float getData()
        {
            return data;
        }

        public void setData(float data)
        {
            this.data = data;
        }

        public FloatTag(string name) : base(name) { }

        public FloatTag(string name, float data) : base(name)
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
            return TAG_Float;
        }

        public override string toString()
        {
            return string.Format("FloatTag {0} (data: {1})", getName(), data);
        }

        public override Tag Copy()
        {
            return new FloatTag(getName(), data);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                FloatTag o = obj as FloatTag;
                return data == o.data;
            }
            return false;
        }
    }
}
