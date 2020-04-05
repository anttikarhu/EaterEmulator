﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JC : Operation
    {
        public const byte OP_CODE = 0b01110000;

        public JC(Emulator emulator) : base(emulator)
        {
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte dstAddress)
        {
            if (emulator.Flags.Carry)
            {
                emulator.ProgramCounter = dstAddress;
            }
        }
    }
}
