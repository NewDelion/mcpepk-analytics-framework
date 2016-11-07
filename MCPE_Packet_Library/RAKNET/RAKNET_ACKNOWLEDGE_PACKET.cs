using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCPE_Packet_Library.RAKNET
{
    public abstract class RAKNET_ACKNOWLEDGE_PACKET : RAKNET_BASE
    {
        public Dictionary<int, int> packets;

        public override void encode()
        {
            base.encode();
            int count = this.packets.Count;
            
            short records = 0;
            byte[] payload = new byte[0];

            if(count > 0)
            {
                using(MemoryStream stream = new MemoryStream())
                {
                    int pointer = 1;
                    int start = packets[0];
                    int last = packets[0];

                    while(pointer < count)
                    {
                        int current = packets[pointer++];
                        int diff = current - last;
                        if (diff == 1)
                            last = current;
                        else if (diff > 1)
                        {
                            if (start == last)
                            {
                                stream.WriteByte((byte)0x01);
                                stream.Write(Binary.writeLTriad(start), 0, 3);
                                start = last = current;
                            }
                            else
                            {
                                stream.WriteByte((byte)0x00);
                                stream.Write(Binary.writeLTriad(start), 0, 3);
                                stream.Write(Binary.writeLTriad(last), 0, 3);
                                start = last = current;
                            }
                            ++records;
                        }
                    }
                    if(start == last)
                    {
                        stream.WriteByte((byte)0x01);
                        stream.Write(Binary.writeLTriad(start), 0, 3);
                    }
                    else
                    {
                        stream.WriteByte((byte)0x01);
                        stream.Write(Binary.writeLTriad(start), 0, 3);
                        stream.Write(Binary.writeLTriad(last), 0, 3);
                    }
                    ++records;

                    payload = stream.ToArray();
                }
            }

            this.putSignedShort(records);
            this.buffer = Binary.appendBytes(this.buffer, payload);
        }

        public override void decode()
        {
            base.decode();
            short count = this.getSignedShort();
            this.packets = new Dictionary<int, int>();
            int cnt = 0;
            for(int i = 0; i < count && !this.feof() && cnt < 4096; ++i)
            {
                if (this.getByte() == 0)
                {
                    int start = this.getLTriad();
                    int end = this.getLTriad();
                    if ((end - start) > 512)
                        end = start + 512;
                    for (int c = start; c <= end; ++c)
                        packets.Add(cnt++, c);
                }
                else
                {
                    this.packets.Add(cnt++, this.getLTriad());
                }
            }
        }
    }
}
