using EaterEmulator.Operations;
using EaterEmulator.Registers;
using NUnit.Framework;

namespace EaterEmulator.Tests.Operations
{
    public class OperationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLDA()
        {
            SignalBus signals = new SignalBus();
            Operation op = new LDA(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.MI);

            signals.Reset();
            op.Step3();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.AI);
        }

        [Test]
        public void TestSTA()
        {
            SignalBus signals = new SignalBus();
            Operation op = new STA(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.MI);

            signals.Reset();
            op.Step3();
            Assert.IsTrue(signals.AO);
            Assert.IsTrue(signals.RI);
        }

        [Test]
        public void TestOUT()
        {
            SignalBus signals = new SignalBus();
            Operation op = new OUT(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.AO);
            Assert.IsTrue(signals.OI);
        }

        [Test]
        public void TestHLT()
        {
            SignalBus signals = new SignalBus();
            Operation op = new HLT(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.HLT);
        }

        [Test]
        public void TestADD()
        {
            SignalBus signals = new SignalBus();
            Operation op = new ADD(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.MI);

            signals.Reset();
            op.Step3();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.BI);

            signals.Reset();
            op.Step4();
            Assert.IsTrue(signals.EO);
            Assert.IsTrue(signals.AI);
            Assert.IsTrue(signals.FI);
        }

        [Test]
        public void TestJCCarryEnabled()
        {
            Emulator emulator = new Emulator();
            SignalBus signals = emulator.signals;
            emulator.flags.Value = FlagsRegister.CARRY;
            Operation op = new JC(emulator, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.J);
        }

        [Test]
        public void TestJCCarryDisabled()
        {
            Emulator emulator = new Emulator();
            SignalBus signals = emulator.signals;
            emulator.flags.Value = 0;
            Operation op = new JC(emulator, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsFalse(signals.IO);
            Assert.IsFalse(signals.J);
        }

        [Test]
        public void TestJMP()
        {
            Emulator emulator = new Emulator();
            SignalBus signals = emulator.signals;
            emulator.flags.Value = FlagsRegister.CARRY;
            Operation op = new JMP(emulator, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.J);
        }

        [Test]
        public void TestJZZeroEnabled()
        {
            Emulator emulator = new Emulator();
            SignalBus signals = emulator.signals;
            emulator.flags.Value = FlagsRegister.ZERO;
            Operation op = new JZ(emulator, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.J);
        }

        [Test]
        public void TestJZZeroDisabled()
        {
            Emulator emulator = new Emulator();
            SignalBus signals = emulator.signals;
            emulator.flags.Value = 0;
            Operation op = new JZ(emulator, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsFalse(signals.IO);
            Assert.IsFalse(signals.J);
        }


        [Test]
        public void TestLDI()
        {
            SignalBus signals = new SignalBus();
            Operation op = new LDI(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.AI);
        }

        [Test]
        public void TestSUB()
        {
            SignalBus signals = new SignalBus();
            Operation op = new SUB(null, signals);

            signals.Reset();
            op.Step0();
            Assert.IsTrue(signals.MI);
            Assert.IsTrue(signals.CO);

            signals.Reset();
            op.Step1();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.II);
            Assert.IsTrue(signals.CE);

            signals.Reset();
            op.Step2();
            Assert.IsTrue(signals.IO);
            Assert.IsTrue(signals.MI);

            signals.Reset();
            op.Step3();
            Assert.IsTrue(signals.RO);
            Assert.IsTrue(signals.BI);

            signals.Reset();
            op.Step4();
            Assert.IsTrue(signals.EO);
            Assert.IsTrue(signals.AI);
            Assert.IsTrue(signals.SU);
            Assert.IsTrue(signals.FI);
        }

        [Test]
        public void ConvertsOpCodeToOperation()
        {
            Emulator emulator = new Emulator();

            Assert.IsTrue(emulator.GetOperation(0b00000000) is NOP);
            Assert.IsTrue(emulator.GetOperation(0b00010000) is LDA);
            Assert.IsTrue(emulator.GetOperation(0b00100000) is ADD);
            Assert.IsTrue(emulator.GetOperation(0b00110000) is SUB);
            Assert.IsTrue(emulator.GetOperation(0b01000000) is STA);
            Assert.IsTrue(emulator.GetOperation(0b01010000) is LDI);
            Assert.IsTrue(emulator.GetOperation(0b01100000) is JMP);
            Assert.IsTrue(emulator.GetOperation(0b01110000) is JC);
            Assert.IsTrue(emulator.GetOperation(0b10000000) is JZ);
            Assert.IsTrue(emulator.GetOperation(0b10010000) is NOP);
            Assert.IsTrue(emulator.GetOperation(0b10100000) is NOP);
            Assert.IsTrue(emulator.GetOperation(0b10110000) is NOP);
            Assert.IsTrue(emulator.GetOperation(0b11000000) is NOP);
            Assert.IsTrue(emulator.GetOperation(0b11010000) is NOP);
            Assert.IsTrue(emulator.GetOperation(0b11100000) is OUT);
            Assert.IsTrue(emulator.GetOperation(0b11110000) is HLT);
        }
    }
}