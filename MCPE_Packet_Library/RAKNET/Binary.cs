using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Packet_Library.RAKNET
{
    public class Binary
    {
        public static bool readBool(byte b)
        {
            return b == 0;
        }

        public static byte writeBool(bool b)
        {
            return (byte)(b ? 0x01 : 0x00);
        }

        public static int readSignedByte(byte b)
        {
            return b & 0xff;
        }

        public static byte writeByte(byte b)
        {
            return b;
        }

        public static int readShort(byte[] bytes)
        {
            return ((bytes[0] & 0xff) << 8) + (bytes[1] & 0xff);
        }

        public static short readSignedShort(byte[] bytes)
        {
            return (short)readShort(bytes);
        }

        public static byte[] writeShort(int s)
        {
            byte[] buffer = new byte[2];
            buffer[0] = (byte)(s >> 8);
            buffer[1] = (byte)s;
            return buffer;
        }

        public static int readLShort(byte[] bytes)
        {
            return ((bytes[1] & 0xff) << 8) + (bytes[0] & 0xff);
        }

        public static short readSignedLShort(byte[] bytes)
        {
            return (short)readLShort(bytes);
        }

        public static byte[] writeLShort(int s)
        {
            byte[] buffer = new byte[2];
            buffer[0] = (byte)s;
            buffer[1] = (byte)(s >> 8);
            return buffer;
        }

        public static int readInt(byte[] bytes)
        {
            return 
                ((bytes[0] & 0xff) << 24) +
                ((bytes[1] & 0xff) << 16) +
                ((bytes[2] & 0xff) << 8) +
                (bytes[3] & 0xff);
        }

        public static byte[] writeInt(int i)
        {
            byte[] buffer = new byte[4];
            buffer[0] = (byte)(i >> 24);
            buffer[1] = (byte)(i >> 16);
            buffer[2] = (byte)(i >> 8);
            buffer[3] = (byte)i;
            return buffer;
        }

        public static int readLInt(byte[] bytes)
        {
            return
                ((bytes[3] & 0xff) << 24) +
                ((bytes[2] & 0xff) << 16) +
                ((bytes[1] & 0xff) << 8) +
                (bytes[0] & 0xff);
        }

        public static byte[] writeLInt(int i)
        {
            byte[] buffer = new byte[4];
            buffer[3] = (byte)i;
            buffer[0] = (byte)(i >> 8);
            buffer[1] = (byte)(i >> 16);
            buffer[2] = (byte)(i >> 24);
            return buffer;
        }

        public static long readLong(byte[] bytes)
        {
            return
                ((long)bytes[0] << 56) +
                ((long)(bytes[1] & 0xff) << 48) +
                ((long)(bytes[2] & 0xff) << 40) +
                ((long)(bytes[3] & 0xff) << 32) +
                ((long)(bytes[4] & 0xff) << 24) +
                ((bytes[5] & 0xff) << 16) +
                ((bytes[6] & 0xff) << 8) +
                ((bytes[7] & 0xff));
        }

        public static byte[] writeLong(long l)
        {
            byte[] buffer = new byte[8];
            buffer[0] = (byte)(l >> 56);
            buffer[1] = (byte)(l >> 48);
            buffer[2] = (byte)(l >> 40);
            buffer[3] = (byte)(l >> 32);
            buffer[4] = (byte)(l >> 24);
            buffer[5] = (byte)(l >> 16);
            buffer[6] = (byte)(l >> 8);
            buffer[7] = (byte)l;
            return buffer;
        }

        public static long readLLong(byte[] bytes)
        {
            return
                ((long)bytes[7] << 56) +
                ((long)(bytes[6] & 0xff) << 48) +
                ((long)(bytes[5] & 0xff) << 40) +
                ((long)(bytes[4] & 0xff) << 32) +
                ((long)(bytes[3] & 0xff) << 24) +
                ((bytes[2] & 0xff) << 16) +
                ((bytes[1] & 0xff) << 8) +
                ((bytes[0] & 0xff));
        }

        public static byte[] writeLLong(long l)
        {
            byte[] buffer = new byte[8];
            buffer[0] = (byte)l;
            buffer[1] = (byte)(l >> 8);
            buffer[2] = (byte)(l >> 16);
            buffer[3] = (byte)(l >> 24);
            buffer[4] = (byte)(l >> 32);
            buffer[5] = (byte)(l >> 40);
            buffer[6] = (byte)(l >> 48);
            buffer[7] = (byte)(l >> 56);
            return buffer;
        }

        public static int readTriad(byte[] bytes)
        {
            return readInt(new byte[] { (byte)0x00, bytes[0], bytes[1], bytes[2] });
        }

        public static byte[] writeTriad(int value)
        {
            byte[] buffer = new byte[3];
            buffer[0] = (byte)(value >> 16);
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)value;
            return buffer;
        }

        public static int readLTriad(byte[] bytes)
        {
            return readLInt(new byte[] { bytes[0], bytes[1], bytes[2], (byte)0x00 });
        }

        public static byte[] writeLTriad(int value)
        {
            byte[] buffer = new byte[3];
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            return buffer;
        }

        public static float readFloat(byte[] bytes)
        {
            return BitConverter.ToSingle(new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] }, 0);
        }

        public static byte[] writeFloat(float f)
        {
            return BitConverter.GetBytes(f);
        }

        public static float readLFloat(byte[] bytes)
        {
            return BitConverter.ToSingle(new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] }, 0);
        }

        public static byte[] writeLFloat(float f)
        {
            return BitConverter.GetBytes(f).Reverse().ToArray();
        }

        public static double readDouble(byte[] bytes)
        {
            return BitConverter.Int64BitsToDouble(readLong(bytes));
        }

        public static byte[] writeDouble(double d)
        {
            return writeLong(BitConverter.DoubleToInt64Bits(d));
        }

        public static double readLDouble(byte[] bytes)
        {
            return BitConverter.Int64BitsToDouble(readLLong(bytes));
        }

        public static byte[] writeLDouble(double d)
        {
            return writeLLong(BitConverter.DoubleToInt64Bits(d));
        }

        public static byte[] reverseBytes(byte[] bytes)
        {
            return bytes.Reverse().ToArray();
        }

        public static string bytesToHexString(byte[] src)
        {
            return BitConverter.ToString(src).Replace("-", ":");
        }

        public static string bytesToHexString(byte[] src, bool blank)
        {
            return BitConverter.ToString(src).Replace("-", blank ? ":" : "");
        }

        public static byte[] hexStringToBytes(string hexString)
        {
            string str = "0123456789ABCDEF";
            string target = new string(hexString.ToCharArray().Where(d => str.IndexOf(d) >= 0).ToArray());
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                for (int i = 0; i < target.Length; i += 2)
                {
                    string num = target[i].ToString() + target[i + 1];
                    stream.WriteByte((byte)Convert.ToInt32(num, 16));
                }
                return stream.ToArray();
            }
        }

        public static byte[] subBytes(byte[] bytes, int start, int length)
        {
            if (start >= bytes.Length)
                return new byte[0];
            if (start + length > bytes.Length)
                return bytes.Skip(start).ToArray();
            return bytes.Skip(start).Take(length).ToArray();
        }

        public static byte[] subBytes(byte[] bytes, int start)
        {
            if (start >= bytes.Length)
                return new byte[0];
            return bytes.Skip(start).ToArray();
        }

        public static byte[][] splitBytes(byte[] bytes, int chunkSize)
        {
            List<byte[]> result = new List<byte[]>();
            for(int i = 0;i< (int)((bytes.Length + 1) / 2); i++)
            {
                if ((i + 1) * chunkSize > bytes.Length)
                    result.Add(bytes.Skip(i * chunkSize).ToArray());
                else
                    result.Add(bytes.Skip(i * chunkSize).Take(chunkSize).ToArray());
            }
            return result.ToArray();
        }

        public static byte[] appendBytes(byte[][] bytes)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                foreach (byte[] bytes2 in bytes)
                    stream.Write(bytes2, 0, bytes2.Length);
                return stream.ToArray();
            }
        }

        public static byte[] appendBytes(byte byte1, params byte[][] byte2)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                stream.WriteByte(byte1);
                foreach (byte[] bytes in byte2)
                    stream.Write(bytes, 0, bytes.Length);
                return stream.ToArray();
            }
        }

        public static byte[] appendBytes(byte[] byte1, params byte[][] byte2)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                stream.Write(byte1, 0, byte1.Length);
                foreach (byte[] bytes in byte2)
                    stream.Write(bytes, 0, bytes.Length);
                return stream.ToArray();
            }
        }

        public static Guid readUUID(byte[] bytes)
        {
            return new Guid(bytes);
        }

        public static byte[] writeUUID(Guid uuid)
        {
            return uuid.ToByteArray();
        }
    }
}
