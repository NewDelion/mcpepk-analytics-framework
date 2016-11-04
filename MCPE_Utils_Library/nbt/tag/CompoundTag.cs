using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class CompoundTag : Tag
    {
        private Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public CompoundTag() : base("") { }

        public CompoundTag(string name) : base(name) { }

        protected internal override void write(BinaryWriter dos)
        {
            foreach (Tag tag in tags.Values)
                Tag.writeNamedTag(tag, dos);
            dos.Write(Tag.TAG_End);
        }

        protected internal override void load(BinaryReader dis)
        {
            tags.Clear();
            Tag tag;
            while ((tag = Tag.readNamedTag(dis)).getId() != Tag.TAG_End)
                tags.Add(tag.getName(), tag);
        }

        public List<Tag> getAllTags()
        {
            return tags.Values.ToList();
        }

        public override byte getId()
        {
            return TAG_Compound;
        }

        public CompoundTag put(string name, Tag tag)
        {
            tags.Add(name, tag.setName(name));
            return this;
        }

        public CompoundTag putByte(string name, int value)
        {
            tags.Add(name, new ByteTag(name, value));
            return this;
        }

        public CompoundTag putShort(string name, int value)
        {
            tags.Add(name, new ShortTag(name, value));
            return this;
        }

        public CompoundTag putInt(string name, int value)
        {
            tags.Add(name, new IntTag(name, value));
            return this;
        }

        public CompoundTag putLong(string name, long value)
        {
            tags.Add(name, new LongTag(name, value));
            return this;
        }

        public CompoundTag putFloat(string name, float value)
        {
            tags.Add(name, new FloatTag(name, value));
            return this;
        }

        public CompoundTag putDouble(string name, double value)
        {
            tags.Add(name, new DoubleTag(name, value));
            return this;
        }

        public CompoundTag putString(string name, string value)
        {
            tags.Add(name, new StringTag(name, value));
            return this;
        }

        public CompoundTag putByteArray(string name, byte[] value)
        {
            tags.Add(name, new ByteArrayTag(name, value));
            return this;
        }

        public CompoundTag putIntArray(string name, int[] value)
        {
            tags.Add(name, new IntArrayTag(name, value));
            return this;
        }

        public CompoundTag putList<T>(ListTag<T> listTag) where T : Tag
        {
            tags.Add(listTag.getName(), listTag);
            return this;
        }

        public CompoundTag putCompound(string name, CompoundTag value)
        {
            tags.Add(name, value.setName(name));
            return this;
        }

        public CompoundTag putBoolean(string name, bool val)
        {
            putByte(name, val ? 1 : 0);
            return this;
        }

        public Tag get(string name)
        {
            if (tags.ContainsKey(name))
                return tags[name];
            return null;
        }

        public bool Contains(string name)
        {
            return tags.ContainsKey(name);
        }

        public CompoundTag Remove(string name)
        {
            tags.Remove(name);
            return this;
        }

        public int getByte(string name)
        {
            if (!tags.ContainsKey(name)) return (byte)0x00;
            return (tags[name] as INumberTag<int>).getData();
        }

        public int getShort(string name)
        {
            if (!tags.ContainsKey(name)) return 0;
            return (tags[name] as INumberTag<int>).getData();
        }

        public int getInt(string name)
        {
            if (!tags.ContainsKey(name)) return 0;
            return (tags[name] as INumberTag<int>).getData();
        }

        public long getLong(string name)
        {
            if (!tags.ContainsKey(name)) return 0;
            return (tags[name] as INumberTag<long>).getData();
        }

        public float getFloat(string name)
        {
            if (!tags.ContainsKey(name)) return 0;
            return (tags[name] as INumberTag<float>).getData();
        }

        public double getDouble(string name)
        {
            if (!tags.ContainsKey(name)) return 0;
            return (tags[name] as INumberTag<double>).getData();
        }

        public string getString(string name)
        {
            if (!tags.ContainsKey(name)) return "";
            Tag tag = tags[name];
            if (tag is INumberTag<int>)
                return (tag as INumberTag<int>).getData().ToString();
            else if (tag is INumberTag<long>)
                return (tag as INumberTag<long>).getData().ToString();
            else if (tag is INumberTag<float>)
                return (tag as INumberTag<float>).getData().ToString();
            else if (tag is INumberTag<double>)
                return (tag as INumberTag<double>).getData().ToString();
            return (tag as StringTag).data;
        }

        public byte[] getByteArray(string name)
        {
            if (!tags.ContainsKey(name)) return new byte[0];
            return (tags[name] as ByteArrayTag).data;
        }

        public int[] getIntArray(string name)
        {
            if (!tags.ContainsKey(name)) return new int[0];
            return (tags[name] as IntArrayTag).data;
        }

        public CompoundTag getCompound(string name)
        {
            if (!tags.ContainsKey(name)) return new CompoundTag(name);
            return tags[name] as CompoundTag;
        }

        public ListTag<T> getList<T>(string name) where T : Tag
        {
            if (!tags.ContainsKey(name)) return new ListTag<T>(name);
            return tags[name] as ListTag<T>;
        }

        public Dictionary<string, Tag> getTags()
        {
            return new Dictionary<string, Tag>(this.tags);
        }

        public bool getBoolean(string name)
        {
            return getByte(name) != 0;
        }

        public override string toString()
        {
            return string.Format("CompoundTag {0} ( {1} entries )", getName(), tags.Count);
        }

        public bool isEmpty()
        {
            return tags.Count == 0;
        }

        public override Tag Copy()
        {
            CompoundTag tag = new CompoundTag(getName());
            foreach (string key in tags.Keys)
                tag.put(key, tags[key].Copy());
            return tag;
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                CompoundTag o = obj as CompoundTag;
                return tags.Values.Equals(o.tags.Values);
            }
            return false;
        }
    }
}
