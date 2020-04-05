using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JZ : Operation
    {
        public const byte OP_CODE = 0b10000000;

        public JZ(Emulator emulator) : base(emulator)
        {
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte dstAddress)
        {
            if (emulator.Flags.Zero)
            {
                emulator.ProgramCounter = dstAddress;
            }
        }
    }
}
