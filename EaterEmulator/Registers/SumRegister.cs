using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers
{
    public class SumRegister : Register
    {
        private Emulator emulator;

        public SumRegister(Emulator emulator) : base()
        {
            this.emulator = emulator;
        }

        public override byte Value
        {
            get
            {
                if (Substract)
                {
                    int sum = emulator.A.Value - emulator.B.Value;
                    Carry = sum < 0;
                    Zero = (byte)sum == 0;
                    return (byte)(sum);
                }
                else
                {
                    int sum = emulator.A.Value + emulator.B.Value;
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
