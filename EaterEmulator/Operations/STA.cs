using System;
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

        public override void Step2()
        {
            emulator.signals.IO = true;
            emulator.signals.MI = true;
        }

        public override void Step3()
        {
            emulator.signals.AO = true;
            emulator.signals.RI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
