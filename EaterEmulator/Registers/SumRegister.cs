﻿namespace EaterEmulator.Registers
{
    public class SumRegister : Register
    {
        private Register aRegister;

        private Register bRegister;

        private FlagBus flagBus;

        public SumRegister(Register aRegister, Register bRegister, DataBus bus, SignalBus signals, FlagBus flagBus) : base(bus, signals)
        {
            this.aRegister = aRegister;
            this.bRegister = bRegister;
            this.flagBus = flagBus;
        }

        public override void WriteToBus()
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
                    flagBus.Carry = sum < 0;
                    flagBus.Zero = (byte)sum == 0;
                    return (byte)(sum);
                }
                else
                {
                    int sum = aRegister.Value + bRegister.Value;
                    flagBus.Carry = sum > 255;
                    flagBus.Zero = (byte)sum == 0;
                    return (byte)(sum);
                }
            }
        }

        public bool Substract
        {
            get; set;
        }
    }
}
