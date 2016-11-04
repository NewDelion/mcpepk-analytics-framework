using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCPE_Utils_Library.nbt.tag;
using MCPE_Utils_Library.item;
using System.IO;


namespace MCPE_Utils_Library.nbt
{
    public class NBTIO
    {
        public static CompoundTag putItemHelper(Item item)
        {
            return putItemHelper(item, null);
        }

        public static CompoundTag putItemHelper(Item item, int? slot)
        {
            CompoundTag tag = new CompoundTag(null)
                .putShort("id", item.getId())
                .putByte("Count", item.getCount())
                .putShort("Damage", item.getDamage());
            if (slot != null)
                tag.putByte("Slot", (int)slot);
            if (item.hasCompoundTag())
                tag.putCompound("tag", item.getNamedTag());
            return tag;
        }

        public static Item getItemHelper(CompoundTag tag)
        {
            if (!tag.Contains("id") || !tag.Contains("Count"))
                return Item.get(0);

            Item item = Item.get(tag.getShort("id"), !tag.Contains("Damage") ? 0 : tag.getShort("Damage"), tag.getByte("Count"));

            if (tag.Contains("tag") && tag.get("tag") is CompoundTag)
                item.setNamedTag(tag.getCompound("tag"));
            return item;
        }

        public static CompoundTag read(FileInfo file)
        {
            return read(file, false);
        }

        public static CompoundTag read(FileInfo file, bool little_endian)
        {
            if (!file.Exists) return null;
            return read(file.OpenRead(), little_endian);
        }

        public static CompoundTag read(FileStream stream)
        {
            return read(stream, false);
        }

        public static CompoundTag read(Stream stream, bool little_endian)
        {
            return read(stream, little_endian, false);
        }

        public static CompoundTag read(Stream stream, bool little_endian, bool network)
        {
            using(BinaryReader reader = new BinaryReader(stream))//TODO
            {
                Tag tag = Tag.readNamedTag(reader);
                if (tag is CompoundTag)
                    return tag as CompoundTag;
                throw new IOException("Root tag must be a named compound tag");
            }
        }

        public static CompoundTag read(byte[] data)
        {
            return read(data, false);
        }

        public static CompoundTag read(byte[] data, bool little_endian)
        {
            return read(new MemoryStream(data), little_endian);
        }

        public static CompoundTag read(byte[] data, bool little_endian, bool network)
        {
            return read(new MemoryStream(data), little_endian, network);
        }

        public static byte[] write(CompoundTag tag)
        {
            return write(tag, false);
        }

        public static byte[] write(CompoundTag tag, bool little_endian)
        {
            return write(tag, little_endian, false);
        }

        public static byte[] write(CompoundTag tag, bool little_endian, bool network)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                using(BinaryWriter writer = new BinaryWriter(stream))
                {
                    Tag.writeNamedTag(tag, writer);
                    return stream.ToArray();
                }
            }
        }

        public static byte[] write(List<CompoundTag> tags)
        {
            return write(tags, false);
        }

        public static byte[] write(List<CompoundTag> tags, bool little_endian)
        {
            return write(tags, little_endian, false);
        }

        public static byte[] write(List<CompoundTag> tags, bool little_endian, bool network)
        {
            using(MemoryStream stream =new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    foreach (var tag in tags)
                        Tag.writeNamedTag(tag, writer);
                    return stream.ToArray();
                }
            }
        }
    }
}
