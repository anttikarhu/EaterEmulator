namespace EaterEmulator.Registers
{
    public class FlagsRegister : Register
    {
        private FlagBus flagBus;

        public FlagsRegister(DataBus bus, SignalBus signals, FlagBus flagBus) : base(bus, signals)
        {
            this.flagBus = flagBus;
        }

        public const byte NONE = 0;

        public const byte ZERO = 1;

        public const byte CARRY = 2;

        public override void ReadFromBus()
        {
            if (Signals.FI)
            {
                Value = 0;
                Value += flagBus.Zero ? ZERO : (byte)0;
                Value += flagBus.Carry ? CARRY : (byte)0;
            }
        }

        public bool Zero
        {
            get
            {
                return (Value & ZERO) == ZERO;
            }
        }

        public bool Carry
        {
            get
            {
                return (Value & CARRY) == CARRY;
            }
        }
    }
}
