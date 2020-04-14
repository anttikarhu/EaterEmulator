using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class SUB : Operation
    {
        public const byte OP_CODE = 0b00110000;

        public SUB(Emulator emulator, SignalBus signals, FlagsRegister flags) : base(emulator, signals, flags)
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
            signals.SU = true;
            signals.FI = true;
        }
        public override byte OpCode => OP_CODE;
    }
}
