using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class ADD : Operation
    {
        public const byte OP_CODE = 0b00100000;

        public ADD(Emulator emulator) : base(emulator)
        {
        }

        public override void Step2()
        {
            emulator.Signals.IO = true;
            emulator.Signals.MI = true;
        }

        public override void Step3()
        {
            emulator.Signals.RO = true;
            emulator.Signals.BI = true;
        }

        public override void Step4()
        {
            emulator.Signals.EO = true;
            emulator.Signals.AI = true;
            emulator.Signals.FI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
