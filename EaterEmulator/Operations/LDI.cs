using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class LDI : Operation
    {
        public const byte OP_CODE = 0b01010000;

        public LDI(Emulator emulator) : base(emulator)
        {
        }

        public override void Step2()
        {
            emulator.Signals.IO = true;
            emulator.Signals.AI = true;
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte immediateValue)
        {
            emulator.A.Value = (byte) (0b00001111 & immediateValue);
        }
    }
}
