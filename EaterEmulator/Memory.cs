using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator
{
    public class Memory
    {
        private byte[] data = new byte[16];

        public byte Get(byte address)
        {
            return data[address];
        }

        public void Store(byte address, byte value)
        {
            data[address] = value;
        }
    }
}
