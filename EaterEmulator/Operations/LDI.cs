using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class LDI : Operation
    {
        public const byte OP_CODE = 0b01010000;

        public LDI(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {
        }

        public override void Step2()
        {
            signals.IO = true;
            signals.AI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
