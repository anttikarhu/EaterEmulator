﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public class LDA : Operation
    {
        public const byte OP_CODE = 0b00010000;

        public LDA(Emulator emulator) : base(emulator)
        { 
        }

        public override byte OpCode => OP_CODE;

        public override void Step2()
        {
            emulator.signals.IO = true;
            emulator.signals.MI = true;
        }

        public override void Step3()
        {
            emulator.signals.RO = true;
            emulator.signals.AI = true;
        }
    }
}
