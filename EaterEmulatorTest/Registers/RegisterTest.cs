using EaterEmulator.Operations;
using EaterEmulator.Registers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Registers.Tests
{
    public class RegisterTest
    {
        [Test]
        public void ARegisterInputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register a = new ARegister(bus, signals);
            bus.Value = 255;
            signals.AI = true;

            a.Clk();
            Assert.AreEqual(255, a.Value);
        }

        [Test]
        public void ARegisterOutputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register a = new ARegister(bus, signals);
            a.Value = 255;
            signals.AO = true;

            a.Clk();
            Assert.AreEqual(255, bus.Value);
        }

        [Test]
        public void BRegisterInputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register b = new BRegister(bus, signals);
            bus.Value = 255;
            signals.BI = true;

            b.Clk();
            Assert.AreEqual(255, b.Value);
        }

        [Test]
        public void SumRegisterOutputsSumOfAAndB()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register a = new ARegister(bus, signals);
            Register b = new BRegister(bus, signals);
            Register sum = new SumRegister(a, b, bus, signals);

            a.Value = 11;
            b.Value = 22;

            signals.EO = true;
            sum.Clk();

            Assert.AreEqual(33, bus.Value);
        }

        [Test]
        public void SumRegisterOutputsSubstractionOfAAndB()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register a = new ARegister(bus, signals);
            Register b = new BRegister(bus, signals);
            Register sum = new SumRegister(a, b, bus, signals);

            a.Value = 100;
            b.Value = 1;

            signals.EO = true;
            signals.SU = true;
            sum.Clk();

            Assert.AreEqual(99, bus.Value);
        }

        [Test]
        public void OutputRegisterInputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register output = new OutputRegister(bus, signals);
            bus.Value = 255;
            signals.OI = true;

            output.Clk();
            Assert.AreEqual(255, output.Value);
        }

        [Test]
        public void InstructionRegisterInputsInstructionAndOperand()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register instruction = new InstructionRegister(bus, signals);
            bus.Value = STA.OP_CODE + 15;
            signals.II = true;

            instruction.Clk();
            Assert.AreEqual(STA.OP_CODE + 15, instruction.Value);
        }

        [Test]
        public void InstructionRegisterOutputsOperand()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register instruction = new InstructionRegister(bus, signals);
            instruction.Value = STA.OP_CODE + 15;
            signals.IO = true;

            instruction.Clk();
            Assert.AreEqual(15, bus.Value);
        }

        [Test]
        public void FlagsRegisterInputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register flags = new FlagsRegister(bus, signals);
            bus.Value = FlagsRegister.CARRY + FlagsRegister.ZERO;
            signals.FI = true;

            flags.Clk();
            Assert.AreEqual(FlagsRegister.CARRY + FlagsRegister.ZERO, flags.Value);
        }

        [Test]
        public void MemoryAddressRegisterInputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register memoryAddress = new MemoryAddressRegister(bus, signals);
            bus.Value = 0xF;
            signals.MI = true;

            memoryAddress.Clk();
            Assert.AreEqual(0xF, memoryAddress.Value);
        }
    }
}
