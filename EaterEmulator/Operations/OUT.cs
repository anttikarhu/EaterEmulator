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

        public override void Step2()
        {
            emulator.Signals.AO = true;
            emulator.Signals.OI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
