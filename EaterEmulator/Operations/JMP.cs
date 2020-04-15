using EaterEmulator.Registers;

namespace EaterEmulator.Operations
{
    public class JMP : Operation
    {
        public const byte OP_CODE = 0b01100000;

        public JMP(InstructionCounter instructionCounter, SignalBus signals, FlagsRegister flags) : base(instructionCounter, signals, flags)
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
