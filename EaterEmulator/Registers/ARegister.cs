using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class ARegister : Register
    {
        public ARegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public override void ReadFromBus()
        {
            if (Signals.AI)
            {
                Value = Bus.Value;
            }
        }

        public override void WriteToBus()
        {
            if (Signals.AO)
            {
                Bus.Value = Value;
            }
        }
    }
}
