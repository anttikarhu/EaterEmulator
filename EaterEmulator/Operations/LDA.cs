using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class LDA : Operation
    {
        public const byte OP_CODE = 0b00010000;

        public LDA(Emulator emulator) : base(emulator)
        { 
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte srcAddress)
        {
            emulator.A.Value = emulator.RAM.Get(srcAddress);
        }
    }
}
