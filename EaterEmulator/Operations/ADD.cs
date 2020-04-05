using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class ADD : Operation
    {
        public const byte OP_CODE = 0b00100000;

        public ADD(Emulator emulator) : base(emulator)
        {
        }

        public override byte OpCode => OP_CODE;

        public override void Run(byte srcAddress)
        {
            emulator.B.Value = emulator.RAM.Get(srcAddress);
            byte flags = 0;

            byte sum = emulator.Sum.Value;

            if (emulator.Sum.Carry)
            {
                flags = (byte)(flags + FlagsRegister.CARRY);
            }
            if (emulator.Sum.Zero)
            {
                flags = (byte)(flags + FlagsRegister.ZERO);
            }

            emulator.A.Value = sum;
            emulator.Flags.Value = flags;
        }
    }
}
