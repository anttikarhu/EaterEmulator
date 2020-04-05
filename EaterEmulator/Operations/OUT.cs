using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class OUT : Operation
    {
        public const byte OP_CODE = 0b11100000;

        public OUT(Emulator emulator) : base(emulator)
        { 
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte none)
        {
            emulator.Output.Value = emulator.A.Value;
        }
    }
}
