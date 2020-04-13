using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class BEQ : Operation
    {
        public const byte OP_CODE = 0b10010000;

        public BEQ(Emulator emulator) : base(emulator)
        {
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte dstAddress)
        {
            if (emulator.A.Value == emulator.B.Value)
            {
                emulator.ProgramCounter.Value = dstAddress;
            }
        }
    }
}
