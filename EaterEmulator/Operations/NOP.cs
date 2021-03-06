﻿using EaterEmulator.Registers;

namespace EaterEmulator.Operations
{
    public class NOP : Operation
    {
        public const byte OP_CODE = 0b00000000;

        public NOP(InstructionCounter instructionCounter, SignalBus signals, FlagsRegister flags) : base(instructionCounter, signals, flags)
        {
        }

        public override byte OpCode => OP_CODE;
    }
}
