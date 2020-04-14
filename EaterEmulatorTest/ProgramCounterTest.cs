using EaterEmulator.Registers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Tests
{
    public class ProgramCounterTest
    {
        [Test]
        public void ProgramCounterIncrements()
        {
            SignalBus signals = new SignalBus();
            ProgramCounter programCounter = new ProgramCounter(null, signals);

            signals.CE = true;

            programCounter.ReadFromBus();

            Assert.AreEqual(1, programCounter.Value);
        }

        [Test]
        public void ProgramCounterWraps()
        {
            SignalBus signals = new SignalBus();
            ProgramCounter programCounter = new ProgramCounter(null, signals);

            programCounter.Value = 15;
            signals.CE = true;

            programCounter.ReadFromBus();

            Assert.AreEqual(0, programCounter.Value);
        }

        [Test]
        public void ProgramCounterOutputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();
            ProgramCounter programCounter = new ProgramCounter(bus, signals);

            programCounter.Value = 15;
            signals.CO = true;

            programCounter.WriteToBus();

            Assert.AreEqual(15, bus.Value);
        }
    }
}
