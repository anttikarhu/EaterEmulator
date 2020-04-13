using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JMP : Operation
    {
        public const byte OP_CODE = 0b01100000;

        public JMP(Emulator emulator) : base(emulator)
        {
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte dstAddress)
        {
            emulator.ProgramCounter.Value = dstAddress;
        }
    }
}
