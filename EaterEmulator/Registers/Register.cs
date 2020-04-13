using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class Register
    {
        public DataBus Bus { get; internal set; }

        public SignalBus Signals { get; internal set; }

        public Register(DataBus bus, SignalBus signals)
        {
            this.Bus = bus;
            this.Signals = signals;
        }

        public virtual byte Value { get; set; }

        public virtual void Clk() { }
    }
}
