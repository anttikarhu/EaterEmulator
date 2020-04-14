using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class NOP : Operation
    {
        public const byte OP_CODE = 0b00000000;

        public NOP(Emulator emulator, SignalBus signals) : base(emulator, signals)
        {
        }

        public override byte OpCode => OP_CODE;
    }
}
