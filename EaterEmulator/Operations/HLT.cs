using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class HLT : Operation
    {
        public const byte OP_CODE = 0b11110000;

        public HLT(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {

        }

        public override void Step2()
        {
            signals.HLT = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
