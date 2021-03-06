﻿namespace EaterEmulator.Registers
{
    public class MemoryAddressRegister : Register
    {
        public MemoryAddressRegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public override void ReadFromBus()
        {
            if (Signals.MI)
            {
                Value = Bus.Value;
            }
        }
    }
}
