using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class SumRegister : Register
    {
        private Register aRegister;

        private Register bRegister;

        public SumRegister(Register aRegister, Register bRegister, DataBus bus, SignalBus signals) : base(bus, signals)
        {
            this.aRegister = aRegister;
            this.bRegister = bRegister;
        }

        public override void Clk()
        {
            if (Signals.SU)
            {
                Substract = true;
            }

            if (Signals.EO)
            {
                Bus.Value = Value;
            }
        }

        public override byte Value
        {
            get
            {
                if (Substract)
                {
                    int sum = aRegister.Value - bRegister.Value;
                    Carry = sum < 0;
                    Zero = (byte)sum == 0;
                    return (byte)(sum);
                }
                else
                {
                    int sum = aRegister.Value + bRegister.Value;
                    Carry = sum > 255;
                    Zero = (byte)sum == 0;
                    return (byte)(sum);
                }
            }
        }

        public bool Zero
        {
            get; internal set;
        }

        public bool Carry
        {
            get; internal set;
        }

        public bool Substract
        {
            get; set;
        }
    }
}
