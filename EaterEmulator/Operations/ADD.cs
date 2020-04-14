using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class ADD : Operation
    {
        public const byte OP_CODE = 0b00100000;

        public ADD(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {
        }

        public override void Step2()
        {
            signals.IO = true;
            signals.MI = true;
        }

        public override void Step3()
        {
            signals.RO = true;
            signals.BI = true;
        }

        public override void Step4()
        {
            signals.EO = true;
            signals.AI = true;
            signals.FI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
