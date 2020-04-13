using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JC : Operation
    {
        public const byte OP_CODE = 0b01110000;

        public JC(Emulator emulator) : base(emulator)
        {
        }

        public override void Step2()
        {
            if (emulator.Flags.Carry)
            {
                emulator.Signals.IO = true;
                emulator.Signals.J = true;
            }
        }
        public override byte OpCode => OP_CODE;

        public override void Run(byte dstAddress)
        {
            if (emulator.Flags.Carry)
            {
                emulator.ProgramCounter.Value = dstAddress;
            }
        }
    }
}
