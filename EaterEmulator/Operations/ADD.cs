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
            emulator.signals.IO = true;
            emulator.signals.MI = true;
        }

        public override void Step3()
        {
            emulator.signals.RO = true;
            emulator.signals.BI = true;
        }

        public override void Step4()
        {
            emulator.signals.EO = true;
            emulator.signals.AI = true;
            emulator.signals.FI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
