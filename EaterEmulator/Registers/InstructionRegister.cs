using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class InstructionRegister : Register
    {
        public InstructionRegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public override void ReadFromBus()
        {
            if (Signals.II)
            {
                Value = Bus.Value;
            }
        }

        public override void WriteToBus()
        {
            if (Signals.IO)
            {
                Bus.Value = (byte)(Value & 0b00001111);
            }
        }
    }
}
