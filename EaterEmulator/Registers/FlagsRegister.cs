using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class FlagsRegister : Register
    {
        public FlagsRegister(DataBus bus, SignalBus signals) : base(bus, signals)
        {

        }

        public const byte NONE = 0;

        public const byte ZERO = 1;

        public const byte CARRY = 2;


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
