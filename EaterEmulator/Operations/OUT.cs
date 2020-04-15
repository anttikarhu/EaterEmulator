using EaterEmulator.Registers;

namespace EaterEmulator.Operations
{
    public class OUT : Operation
    {
        public const byte OP_CODE = 0b11100000;

        public OUT(InstructionCounter instructionCounter, SignalBus signals, FlagsRegister flags) : base(instructionCounter, signals, flags)
        {
        }

        public override void Step2()
        {
            signals.AO = true;
            signals.OI = true;
        }

        public override byte OpCode => OP_CODE;
    }
}
