using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketSender
{
    struct Message
    {
        /// <summary>
        /// 4 byte char header
        /// </summary>
        public byte[] header;

        public Message(byte[] packet)
        {
            header = new byte[4];
            header[0] = packet[0];
            header[1] = packet[1];
            header[2] = packet[2];
            header[3] = packet[3];
        }
    }
}
