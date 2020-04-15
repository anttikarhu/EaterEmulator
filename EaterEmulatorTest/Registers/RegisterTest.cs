using EaterEmulator.Operations;
using NUnit.Framework;

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

            a.ReadFromBus();
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

            a.WriteToBus();
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

            b.ReadFromBus();
            Assert.AreEqual(255, b.Value);
        }

        [Test]
        public void SumRegisterOutputsSumOfAAndB()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();
            FlagBus flagBus = new FlagBus();

            Register a = new ARegister(bus, signals);
            Register b = new BRegister(bus, signals);
            Register sum = new SumRegister(a, b, bus, signals, flagBus);

            a.Value = 200;
            b.Value = 100;

            signals.EO = true;
            sum.WriteToBus();

            Assert.AreEqual(44, bus.Value);
            Assert.IsTrue(flagBus.Carry);
        }

        [Test]
        public void SumRegisterOutputsSubstractionOfAAndB()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();
            FlagBus flagBus = new FlagBus();

            Register a = new ARegister(bus, signals);
            Register b = new BRegister(bus, signals);
            Register sum = new SumRegister(a, b, bus, signals, flagBus);

            a.Value = 166;
            b.Value = 166;

            signals.EO = true;
            signals.SU = true;
            sum.WriteToBus();

            Assert.AreEqual(0, bus.Value);
            Assert.IsTrue(flagBus.Zero);
        }

        [Test]
        public void OutputRegisterInputs()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register output = new OutputRegister(bus, signals);
            bus.Value = 255;
            signals.OI = true;

            output.ReadFromBus();
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

            instruction.ReadFromBus();
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

            instruction.WriteToBus();
            Assert.AreEqual(15, bus.Value);
        }

        [Test]
        public void FlagsRegisterInputs()
        {
            DataBus bus = new DataBus();
            FlagBus flagBus = new FlagBus();
            SignalBus signals = new SignalBus();

            Register flags = new FlagsRegister(bus, signals, flagBus);
            flagBus.Carry = true;
            flagBus.Zero = true;
            signals.FI = true;

            flags.ReadFromBus();
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

            memoryAddress.ReadFromBus();
            Assert.AreEqual(0xF, memoryAddress.Value);
        }
    }
}
