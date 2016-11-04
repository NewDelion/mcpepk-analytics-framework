using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCPE_Utils_Library.nbt.tag
{
    public abstract class Tag
    {
        public const byte TAG_End = 0;
        public const byte TAG_Byte = 1;
        public const byte TAG_Short = 2;
        public const byte TAG_Int = 3;
        public const byte TAG_Long = 4;
        public const byte TAG_Float = 5;
        public const byte TAG_Double = 6;
        public const byte TAG_Byte_Array = 7;
        public const byte TAG_String = 8;
        public const byte TAG_List = 9;
        public const byte TAG_Compound = 10;
        public const byte TAG_Int_Array = 11;

        private string name;

        protected internal abstract void write(BinaryWriter dos);
        protected internal abstract void load(BinaryReader dis);

        public abstract string toString();

        public abstract byte getId();

        protected internal Tag(string name)
        {
            if (name == null)
                this.name = "";
            else
                this.name = name;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Tag))
                return false;
            Tag o = obj as Tag;
            return getId() == o.getId() && !(name == null && o.name != null || name != null && o.name == null) && !(name != null && !name.Equals(o.name));
        }

        public Tag setName(string name)
        {
            if (name == null)
                this.name = "";
            else
                this.name = name;
            return this;
        }

        public string getName()
        {
            if (name == null) return "";
            return name;
        }

        public static Tag readNamedTag(BinaryReader dis)
        {
            byte type = (byte)dis.ReadByte();
            if (type == 0) return new EndTag();
            
            string name = dis.ReadString();

            Tag tag = newTag(type, name);

            tag.load(dis);
            return tag;
        }

        public static void writeNamedTag(Tag tag, BinaryWriter dos)
        {
            dos.Write((byte)tag.getId());
            if (tag.getId() == Tag.TAG_End) return;
            dos.Write(tag.getName());

            tag.write(dos);
        }

        public static Tag newTag(byte type, string name)
        {
            switch (type)
            {
                case TAG_End:
                    return new EndTag();
                case TAG_Byte:
                    return new ByteTag(name);
                case TAG_Short:
                    return new ShortTag(name);
                case TAG_Int:
                    return new IntTag(name);
                case TAG_Long:
                    return new LongTag(name);
                case TAG_Float:
                    return new FloatTag(name);
                case TAG_Double:
                    return new DoubleTag(name);
                case TAG_Byte_Array:
                    return new ByteArrayTag(name);
                case TAG_Int_Array:
                    return new IntArrayTag(name);
                case TAG_String:
                    return new StringTag(name);
                case TAG_List:
                    return new ListTag<Tag>(name);//
                case TAG_Compound:
                    return new CompoundTag(name);

            }
            return new EndTag();
        }

        public static string getTagName(byte type)
        {
            switch (type)
            {
                case TAG_End:
                    return "TAG_End";
                case TAG_Byte:
                    return "TAG_Byte";
                case TAG_Short:
                    return "TAG_Short";
                case TAG_Int:
                    return "TAG_Int";
                case TAG_Long:
                    return "TAG_Long";
                case TAG_Float:
                    return "TAG_Float";
                case TAG_Double:
                    return "TAG_Double";
                case TAG_Byte_Array:
                    return "TAG_Byte_Array";
                case TAG_Int_Array:
                    return "TAG_Int_Array";
                case TAG_String:
                    return "TAG_String";
                case TAG_List:
                    return "TAG_List";
                case TAG_Compound:
                    return "TAG_Compound";
            }
            return "UNKNOWN";
        }

        public abstract Tag Copy();
    }
}
