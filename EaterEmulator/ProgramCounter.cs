namespace EaterEmulator.Registers
{
    public class ProgramCounter : IBusConnectedModule, IResetableModule
    {
        public byte Value { get; set; }

        private DataBus bus;

        private SignalBus signals;

        public ProgramCounter(DataBus bus, SignalBus signals)
        {
            this.bus = bus;
            this.signals = signals;
        }

        public void ReadFromBus()
        {
            if (signals.CE)
            {
                Value++;

                if (Value >= 16)
                {
                    Value = 0;
                }
            }
            else if (signals.J)
            {
                Value = bus.Value;
            }
        }

        public void WriteToBus()
        {
            if (signals.CO)
            {
                bus.Value = Value;
            }
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}
