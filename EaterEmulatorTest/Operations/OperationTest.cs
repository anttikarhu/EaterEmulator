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
            emulator.RAM.Store(0xF, 62);
            Operation lda = new LDA(emulator);

            lda.Run(0xF);

            Assert.AreEqual(62, emulator.A.Value);
        }

        [Test]
        public void TestSTA()
        {
            Emulator emulator = new Emulator();
            emulator.A.Value = 11;
            Operation sta = new STA(emulator);

            sta.Run(0x0);
            Assert.AreEqual(11, emulator.RAM.Get(0x0));

            emulator.A.Value = 62;
            sta.Run(0xF);
            Assert.AreEqual(62, emulator.RAM.Get(0xF));
        }

        [Test]
        public void TestOUT()
        {
            Emulator emulator = new Emulator();
            emulator.A.Value = 255;
            Operation outOperation = new OUT(emulator);

            outOperation.Run(0);

            Assert.AreEqual(255, emulator.Output.Value);
        }

        [Test]
        public void TestHLT()
        {
            Emulator emulator = new Emulator();
            Operation hlt = new HLT(emulator);

            hlt.Run(0);

            Assert.IsTrue(emulator.IsHalted);
        }

        [Test]
        public void TestADD()
        {
            Emulator emulator = new Emulator();
            emulator.A.Value = 7;
            emulator.RAM.Store(0xF, 55);
            Operation add = new ADD(emulator);

            add.Run(0xF);

            Assert.AreEqual(62, emulator.A.Value);
            Assert.AreEqual(55, emulator.B.Value);
            Assert.AreEqual(117, emulator.Sum.Value);
    
            emulator.A.Value = 1;
            emulator.RAM.Store(0xF, 255);
 
            add.Run(0xF);

            Assert.AreEqual(0, emulator.A.Value);
            Assert.IsTrue(emulator.Flags.Zero);
            Assert.IsTrue(emulator.Flags.Carry);
        }

        [Test]
        public void TestJC()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.NONE;
            Operation jc = new JC(emulator);
            jc.Run(0x2);
            Assert.AreEqual(0x0, emulator.ProgramCounter);

            emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.CARRY;
            jc = new JC(emulator);
            jc.Run(0x2);
            Assert.AreEqual(0x2, emulator.ProgramCounter);
        }

        [Test]
        public void TestJMP()
        {
            Emulator emulator = new Emulator();
            Operation jmp = new JMP(emulator);
            jmp.Run(0x2);
            Assert.AreEqual(0x2, emulator.ProgramCounter);
        }

        [Test]
        public void TestJZ()
        {
            Emulator emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.NONE;
            Operation jz = new JZ(emulator);
            jz.Run(0x2);
            Assert.AreEqual(0x0, emulator.ProgramCounter);

            emulator = new Emulator();
            emulator.Flags.Value = FlagsRegister.ZERO;
            jz = new JZ(emulator);
            jz.Run(0x2);
            Assert.AreEqual(0x2, emulator.ProgramCounter);
        }

        [Test]
        public void TestLDI()
        {
            Emulator emulator = new Emulator();
            Operation ldi = new LDI(emulator);
            ldi.Run(7);
            Assert.AreEqual(7, emulator.A.Value);
        }

        [Test]
        public void TestSUB()
        {
            Emulator emulator = new Emulator();
            emulator.A.Value = 0;
            emulator.RAM.Store(0xF, 1);
            Operation sub = new SUB(emulator);

            sub.Run(0xF);

            Assert.AreEqual(255, emulator.A.Value);
            Assert.IsFalse(emulator.Flags.Zero);
            Assert.IsTrue(emulator.Flags.Carry);

            emulator.A.Value = 1;
            emulator.RAM.Store(0xF, 1);

            sub.Run(0xF);

            Assert.AreEqual(0, emulator.A.Value);
            Assert.IsTrue(emulator.Flags.Zero);
            Assert.IsFalse(emulator.Flags.Carry);
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