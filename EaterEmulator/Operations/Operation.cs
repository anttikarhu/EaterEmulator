using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Operations
{
    public abstract class Operation
    {
        protected Emulator emulator;

        protected SignalBus signals;

        public Operation(Emulator emulator, SignalBus signals)
        {
            this.emulator = emulator;
            this.signals = signals;
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
            signals.MI = true;
            signals.CO = true;
        }

        public virtual void Step1() {
            signals.RO = true;
            signals.II = true;
            signals.CE = true;
        }

        public virtual void Step2() { }

        public virtual void Step3() { }

        public virtual void Step4() { }

        public abstract byte OpCode { get; }
    }
}
