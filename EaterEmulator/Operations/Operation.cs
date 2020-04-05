using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public abstract class Operation
    {
        protected Emulator emulator;

        public Operation(Emulator emulator)
        {
            this.emulator = emulator;
        }

        public abstract void Run(byte operand);

        public abstract byte OpCode { get; }
    }
}
