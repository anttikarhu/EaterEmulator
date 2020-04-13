using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class MemoryAddressRegister : Register
    {
        public MemoryAddressRegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public override void Clk()
        {
            if (Signals.MI)
            {
                Value = Bus.Value;
            }
        }
    }
}
