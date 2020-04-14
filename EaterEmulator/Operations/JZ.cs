using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JZ : Operation
    {
        public const byte OP_CODE = 0b10000000;

        public JZ(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {
        }

        public override void Step2()
        {
            if (emulator.flags.Zero)
            {
                signals.IO = true;
                signals.J = true;
            }
        }

        public override byte OpCode => OP_CODE;
    }
}
