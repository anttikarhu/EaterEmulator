using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JMP : Operation
    {
        public const byte OP_CODE = 0b01100000;

        public JMP(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {
        }

        public override void Step2()
        {
            signals.IO = true;
            signals.J = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
