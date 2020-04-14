using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JC : Operation
    {
        public const byte OP_CODE = 0b01110000;

        public JC(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {
        }

        public override void Step2()
        {
            if (emulator.flags.Carry)
            {
                signals.IO = true;
                signals.J = true;
            }
        }
        public override byte OpCode => OP_CODE;
    }
}
