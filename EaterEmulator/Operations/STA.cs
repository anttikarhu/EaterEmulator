﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class STA : Operation
    {
        public const byte OP_CODE = 0b01000000;

        public STA(Emulator emulator) : base(emulator)
        { 
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte dstAddress)
        {
            emulator.RAM.Store(dstAddress, emulator.A.Value);
        }
    }
}
