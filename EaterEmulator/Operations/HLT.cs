using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class HLT : Operation
    {
        public const byte OP_CODE = 0b11110000;

        public HLT(InstructionCounter instructionCounter, SignalBus signals, FlagsRegister flags) : base(instructionCounter, signals, flags)
        {

        }

        public override void Step2()
        {
            signals.HLT = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
