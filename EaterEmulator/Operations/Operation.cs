﻿using System;
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

        public void Clk() {
            switch (emulator.InstructionCounter)
            {
                case 0:
                    Step0();
                    break;
                case 1:
                    Step1();
                    break;
                case 2:
                    Step2();
                    break;
                case 3:
                    Step3();
                    break;
                case 4:
                    Step4();
                    break;
            }
        }

        public virtual void Step0() {
            emulator.signals.MI = true;
            emulator.signals.CO = true;
        }

        public virtual void Step1() {
            emulator.signals.RO = true;
            emulator.signals.II = true;
            emulator.signals.CE = true;
        }

        public virtual void Step2() { }

        public virtual void Step3() { }

        public virtual void Step4() { }

        public abstract byte OpCode { get; }
    }
}
