using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class EndTag : Tag
    {
        public EndTag() : base(null) { }

        protected internal override void load(BinaryReader dis) { }

        protected internal override void write(BinaryWriter dos) { }

        public override byte getId()
        {
            return TAG_End;
        }

        public override string toString()
        {
            return "EndTag";
        }

        public override Tag Copy()
        {
            return new EndTag();
        }
    }
}
