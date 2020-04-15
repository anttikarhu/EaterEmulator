using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class JC : Operation
    {
        public const byte OP_CODE = 0b01110000;

        public JC(InstructionCounter instructionCounter, SignalBus signals, FlagsRegister flags) : base(instructionCounter, signals, flags)
        {
        }

        public override void Step2()
        {
            if (flags.Carry)
            {
                signals.IO = true;
                signals.J = true;
            }
        }
        public override byte OpCode => OP_CODE;
    }
}
