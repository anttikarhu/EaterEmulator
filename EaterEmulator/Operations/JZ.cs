using EaterEmulator.Registers;

namespace EaterEmulator.Operations
{
    public class JZ : Operation
    {
        public const byte OP_CODE = 0b10000000;

        public JZ(InstructionCounter instructionCounter, SignalBus signals, FlagsRegister flags) : base(instructionCounter, signals, flags)
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
