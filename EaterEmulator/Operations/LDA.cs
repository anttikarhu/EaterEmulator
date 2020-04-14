using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class LDA : Operation
    {
        public const byte OP_CODE = 0b00010000;

        public LDA(Emulator emulator, SignalBus signals) : base(emulator, signals)
        { 
        }

        public override byte OpCode => OP_CODE;

        public override void Step2()
        {
            signals.IO = true;
            signals.MI = true;
        }

        public override void Step3()
        {
            signals.RO = true;
            signals.AI = true;
        }
    }
}
