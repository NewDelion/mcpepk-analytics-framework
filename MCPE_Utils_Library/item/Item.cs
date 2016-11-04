using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCPE_Utils_Library.nbt;
using MCPE_Utils_Library.nbt.tag;

namespace MCPE_Utils_Library.item
{
    public class Item
    {
        private static nbt.tag.CompoundTag parseCompoundTag(byte[] tag)
        {
            return NBTIO.read(tag, true);
        }

        private byte[] writeCompoundTag(nbt.tag.CompoundTag tag)
        {
            return NBTIO.write(tag, true);
        }

        protected internal int id;
        protected internal int meta;
        private bool hasMeta = true;
        private byte[] tags = new byte[0];
        private CompoundTag cachedNBT = null;
        public int count;
        protected internal int durability = 0;
        protected internal string name;

        public Item(int id)
        {
            this.id = id & 0xffff;
            meta = 0;
            this.count = 1;
            this.name = "Unknown";
        }

        public Item(int id, int? meta)
        {
            this.id = id & 0xffff;
            if (meta != null)
                this.meta = (int)meta & 0xffff;
            else
                this.hasMeta = false;
            this.count = 1;
            this.name = "Unknown";
        }

        public Item(int id, int? meta, int count)
        {
            this.id = id & 0xffff;
            if (meta != null)
                this.meta = (int)meta & 0xffff;
            else
                this.hasMeta = false;
            this.count = count;
            this.name = "Unknown";
        }

        public Item(int id, int? meta, int count, string name)
        {
            this.id = id & 0xffff;
            if (meta != null)
                this.meta = (int)meta & 0xffff;
            else
                this.hasMeta = false;
            this.count = count;
            this.name = name;
        }

        public bool HasMeta()
        {
            return hasMeta;
        }

        public bool canBeActivated()
        {
            return false;
        }

        public static void init()
        {
            //TODO
        }

        public static Item get(int id)
        {
            return get(id, 0);
        }

        public static Item get(int id, int? meta)
        {
            return get(id, meta, 1);
        }

        public static Item get(int id, int? meta, int count)
        {
            return get(id, meta, count, new byte[0]);
        }

        public static Item get(int id, int? meta, int count, byte[] tags)
        {
            //TODO
            return new Item(id, meta, count).setCompoundTag(tags);
        }

        public Item setCompoundTag(CompoundTag tag)
        {
            this.setNamedTag(tag);
            return this;
        }

        public Item setCompoundTag(byte[] tags)
        {
            this.tags = tags;
            this.cachedNBT = null;
            return this;
        }

        public byte[] getCompoundTag()
        {
            return tags;
        }

        public bool hasCompoundTag()
        {
            return this.tags != null && this.tags.Length > 0;
        }

        public bool hasCustomBlockData()
        {
            if (!this.hasCompoundTag())
                return false;
            CompoundTag tag = this.getNamedTag();
            return tag.Contains("BlockEntityTag") && tag.get("BlockEntityTag") is CompoundTag;
        }

        public Item clearCustomBlockData()
        {
            if (!this.hasCompoundTag())
                return this;
            CompoundTag tag = this.getNamedTag();

            if(tag.Contains("BlockEntityTag") && tag.get("BlockEntityTag") is CompoundTag)
            {
                tag.Remove("BlockEntityTag");
                this.setNamedTag(tag);
            }

            return this;
        }

        public Item setCustomBlockData(CompoundTag compoundTag)
        {
            CompoundTag tags = compoundTag.Copy() as CompoundTag;
            tags.setName("BlockEntityTag");

            CompoundTag tag;
            if (!this.hasCompoundTag())
                tag = new CompoundTag();
            else
                tag = this.getNamedTag();

            tag.putCompound("BlockEntityTag", tags);
            this.setNamedTag(tag);

            return this;
        }

        public CompoundTag getCustomBlockData()
        {
            if (!this.hasCompoundTag())
                return null;

            CompoundTag tag = this.getNamedTag();

            if (tag.Contains("BlockEntityTag"))
            {
                Tag bet = tag.get("BlockEntityTag");
                if (bet is CompoundTag)
                    return bet as CompoundTag;
            }

            return null;
        }

        public bool hasCustomName()
        {
            if (!this.hasCompoundTag())
                return false;

            CompoundTag tag = this.getNamedTag();
            if (tag.Contains("display"))
            {
                Tag tag1 = tag.get("display");
                if (tag1 is CompoundTag && (tag1 as CompoundTag).Contains("Name") && (tag1 as CompoundTag).get("Name") is StringTag)
                    return true;
            }

            return false;
        }

        public string getCustomName()
        {
            if (!this.hasCompoundTag())
                return "";

            CompoundTag tag = this.getNamedTag();
            if (tag.Contains("display"))
            {
                Tag tag1 = tag.get("display");
                if (tag1 is CompoundTag && (tag1 as CompoundTag).Contains("Name") && (tag1 as CompoundTag).get("Name") is StringTag)
                    return (tag1 as CompoundTag).getString("Name");
            }

            return "";
        }

        public Item setCustomName(string name)
        {
            if (name == null || name == "")
                this.clearCustomName();

            CompoundTag tag;
            if (!this.hasCompoundTag())
                tag = new CompoundTag();
            else
                tag = this.getNamedTag();
            if (tag.Contains("display") && tag.get("display") is CompoundTag)
                tag.getCompound("display").putString("Name", name);
            else
                tag.putCompound("display", new CompoundTag("display").putString("Name", name));
            this.setNamedTag(tag);
            return this;
        }

        public Item clearCustomName()
        {
            if (!this.hasCompoundTag())
                return this;

            CompoundTag tag = this.getNamedTag();

            if (tag.Contains("display") && tag.get("display") is CompoundTag)
            {
                tag.getCompound("display").Remove("Name");
                if (tag.getCompound("display").isEmpty())
                    tag.Remove("display");
                this.setNamedTag(tag);
            }

            return this;
        }

        public Tag getNamedTagEntry(string name)
        {
            CompoundTag tag = this.getNamedTag();
            if (tag != null)
                return tag.Contains(name) ? tag.get(name) : null;
            return null;
        }

        public CompoundTag getNamedTag()
        {
            if (!this.hasCompoundTag())
                return null;
            else if (this.cachedNBT != null)
                return this.cachedNBT;
            return this.cachedNBT = parseCompoundTag(this.tags);
        }

        public Item setNamedTag(CompoundTag tag)
        {
            if (tag.isEmpty())
                return this.clearNamedTag();

            this.cachedNBT = tag;
            this.tags = writeCompoundTag(tag);

            return this;
        }

        public Item clearNamedTag()
        {
            return this.setCompoundTag(new byte[0]);
        }

        public int getCount()
        {
            return count;
        }

        public string getName()
        {
            return this.hasCustomName() ? this.getCustomName() : this.name;
        }

        public int getId()
        {
            return id;
        }

        public int getDamage()
        {
            return meta;
        }

        public void setDamage(int? meta)
        {
            if (meta != null)
                this.meta = (int)meta & 0xffff;
            else
                this.hasMeta = false;
        }

        public int getMaxStackSize()
        {
            return 64;
        }
    }
}
