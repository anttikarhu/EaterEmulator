using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class OutputRegister : Register
    {
        public OutputRegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public override void ReadFromBus()
        {
            if (Signals.OI)
            {
                Value = Bus.Value;
            }
        }
    }
}
