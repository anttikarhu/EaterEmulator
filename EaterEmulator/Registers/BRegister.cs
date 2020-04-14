using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class BRegister : Register
    {
        public BRegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public override void ReadFromBus()
        {
            if (Signals.BI)
            {
                Value = Bus.Value;
            }
        }
    }
}
