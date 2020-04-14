using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JZ : Operation
    {
        public const byte OP_CODE = 0b10000000;

        public JZ(Emulator emulator, SignalBus signals, FlagsRegister flags) : base(emulator, signals, flags)
        {
        }

        public override void Step2()
        {
            if (flags.Zero)
            {
                signals.IO = true;
                signals.J = true;
            }
        }

        public override byte OpCode => OP_CODE;
    }
}
