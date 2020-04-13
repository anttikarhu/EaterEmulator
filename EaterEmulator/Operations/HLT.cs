using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class HLT : Operation
    {
        public const byte OP_CODE = 0b11110000;

        public HLT(Emulator emulator) : base(emulator)
        {

        }

        public override void Step2()
        {
            emulator.IsHalted = true;
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte none)
        {
            emulator.IsHalted = true;
        }
    }
}
