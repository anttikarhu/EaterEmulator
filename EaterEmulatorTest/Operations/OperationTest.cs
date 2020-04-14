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
            Emulator emulator = new Emulator();
            Operation op = new LDA(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.MI);

            emulator.signals.Reset();
            op.Step3();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.AI);
        }

        [Test]
        public void TestSTA()
        {
            Emulator emulator = new Emulator();
            Operation op = new STA(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.MI);

            emulator.signals.Reset();
            op.Step3();
            Assert.IsTrue(emulator.signals.AO);
            Assert.IsTrue(emulator.signals.RI);
        }

        [Test]
        public void TestOUT()
        {
            Emulator emulator = new Emulator();
            Operation op = new OUT(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.AO);
            Assert.IsTrue(emulator.signals.OI);
        }

        [Test]
        public void TestHLT()
        {
            Emulator emulator = new Emulator();
            Operation op = new HLT(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.HLT);
        }

        [Test]
        public void TestADD()
        {
            Emulator emulator = new Emulator();
            Operation op = new ADD(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.MI);

            emulator.signals.Reset();
            op.Step3();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.BI);

            emulator.signals.Reset();
            op.Step4();
            Assert.IsTrue(emulator.signals.EO);
            Assert.IsTrue(emulator.signals.AI);
            Assert.IsTrue(emulator.signals.FI);
        }

        [Test]
        public void TestJCCarryEnabled()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.CARRY;
            Operation op = new JC(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.J);
        }

        [Test]
        public void TestJCCarryDisabled()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = 0;
            Operation op = new JC(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsFalse(emulator.signals.IO);
            Assert.IsFalse(emulator.signals.J);
        }

        [Test]
        public void TestJMP()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.CARRY;
            Operation op = new JMP(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.J);
        }

        [Test]
        public void TestJZZeroEnabled()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.ZERO;
            Operation op = new JZ(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.J);
        }

        [Test]
        public void TestJZZeroDisabled()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = 0;
            Operation op = new JZ(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsFalse(emulator.signals.IO);
            Assert.IsFalse(emulator.signals.J);
        }


        [Test]
        public void TestLDI()
        {
            Emulator emulator = new Emulator();
            Operation op = new LDI(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.AI);
        }

        [Test]
        public void TestSUB()
        {
            Emulator emulator = new Emulator();
            Operation op = new SUB(emulator);

            emulator.signals.Reset();
            op.Step0();
            Assert.IsTrue(emulator.signals.MI);
            Assert.IsTrue(emulator.signals.CO);

            emulator.signals.Reset();
            op.Step1();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.II);
            Assert.IsTrue(emulator.signals.CE);

            emulator.signals.Reset();
            op.Step2();
            Assert.IsTrue(emulator.signals.IO);
            Assert.IsTrue(emulator.signals.MI);

            emulator.signals.Reset();
            op.Step3();
            Assert.IsTrue(emulator.signals.RO);
            Assert.IsTrue(emulator.signals.BI);

            emulator.signals.Reset();
            op.Step4();
            Assert.IsTrue(emulator.signals.EO);
            Assert.IsTrue(emulator.signals.AI);
            Assert.IsTrue(emulator.signals.SU);
            Assert.IsTrue(emulator.signals.FI);
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