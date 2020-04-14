using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class STA : Operation
    {
        public const byte OP_CODE = 0b01000000;

        public STA(Emulator emulator, SignalBus signals) : base(emulator, signals)
        { 
        }

        public override void Step2()
        {
            signals.IO = true;
            signals.MI = true;
        }

        public override void Step3()
        {
            signals.AO = true;
            signals.RI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
