using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public class ListTag<T> : Tag where T : Tag
    {
        private List<T> list = new List<T>();

        public byte type;

        public ListTag() : base("") { }

        public ListTag(string name) : base(name) { }

        protected internal override void write(BinaryWriter dos)
        {
            if (list.Count > 0) type = list[0].getId();
            else type = 1;

            dos.Write(type);
            dos.Write(list.Count);
            foreach (T aList in list)
                aList.write(dos);
        }

        protected internal override void load(BinaryReader dis)
        {
            type = dis.ReadByte();
            int size = dis.ReadInt32();

            list = new List<T>();
            for(int i = 0; i < size; i++)
            {
                Tag tag = Tag.newTag(type, null);
                tag.load(dis);
                list.Add((T)tag);
            }
        }

        public override byte getId()
        {
            return TAG_List;
        }

        public override string toString()
        {
            return string.Format("ListTag {0} [ {1} entries of type  {2} ]", getName(), list.Count, Tag.getTagName(type));
        }

        public ListTag<T> Add(T tag)
        {
            type = tag.getId();
            list.Add(tag);
            return this;
        }

        public ListTag<T> Add(int index, T tag)
        {
            type = tag.getId();

            if (index > list.Count)
            {
                for (int i = list.Count; i < index; i++)
                    list.Add(null);
                list.Add(tag);
            }
            else
                list[index] = tag;
            return this;
        }

        public T get(int index)
        {
            return list[index];
        }

        public List<T> getAll()
        {
            return new List<T>(list);
        }

        public void setAll(List<T> tags)
        {
            this.list = new List<T>(tags);
        }

        public void Remove(T tag)
        {
            list.Remove(tag);
        }

        public void RemoveAt(int index)
        {
            if (list.Count > index)
                list.RemoveAt(index);
        }

        public void RemoveAll(List<T> tags)
        {
            foreach (var tag in tags)
                list.Remove(tag);
        }

        public int Size()
        {
            return list.Count;
        }

        public override Tag Copy()
        {
            ListTag<T> res = new ListTag<T>(getName());
            res.type = type;
            res.list.AddRange((T[])list.ToArray().Clone());
            return res;
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ListTag<Tag> o = obj as ListTag<Tag>;
                if (o != null && type == o.type)
                    return list.Equals(o.list);
            }
            return false;
        }
    }
}
