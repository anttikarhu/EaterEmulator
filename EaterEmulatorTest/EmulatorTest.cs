using EaterEmulator.Operations;
using EaterEmulator.Registers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EaterEmulator.Tests
{
    public class EmulatorTest
    {
        [Test]
        public void ClkGetsTheNextInstructionFromRAM()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, 0b00010000);

            emulator.Clk();
            emulator.Clk();

            Assert.AreEqual(LDA.OP_CODE, emulator.instruction.Value);
            Assert.AreEqual(1, emulator.programCounter.Value);
        }

        [Test]
        public void OutputsNumberAndHalts()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0x1, OUT.OP_CODE);
            emulator.RAM.Store(0x2, HLT.OP_CODE);
            emulator.RAM.Store(0xF, 62);

            emulator.Step();
            emulator.Step();
            emulator.Step();

            Assert.IsTrue(emulator.Clock.IsHalted);
            Assert.AreEqual(3, emulator.programCounter.Value);
            Assert.AreEqual(62, emulator.Output.Value);
        }

        [Test]
        public void DoesNotRunWhenHalted()
        {
            Emulator emulator = new Emulator();
            emulator.programCounter.Value = 0;
            emulator.Clock.IsHalted = true;

            emulator.Step();

            Assert.AreEqual(0, emulator.programCounter.Value);
        }

        [Test]
        public void ParsesFlags()
        {
            FlagsRegister flagsRegister = new FlagsRegister(null, null, null);
            flagsRegister.Value = 0;

            Assert.IsFalse(flagsRegister.Zero);
            Assert.IsFalse(flagsRegister.Carry);

            flagsRegister.Value = 3;

            Assert.IsTrue(flagsRegister.Zero);
            Assert.IsTrue(flagsRegister.Carry);
        }

        [Test]
        public void StoresFlags()
        {
            SignalBus signals = new SignalBus();
            FlagBus flagBus = new FlagBus();

            FlagsRegister flagsRegister = new FlagsRegister(null, signals, flagBus);
            flagBus.Carry = true;
            flagBus.Zero = true;
            signals.FI = true;

            flagsRegister.ReadFromBus();

            Assert.AreEqual(3, flagsRegister.Value);
        }

        [Test]
        public void AddsTwoNumbersTogetherAndOutputs()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0x1, ADD.OP_CODE + 0xE);
            emulator.RAM.Store(0x2, OUT.OP_CODE);
            emulator.RAM.Store(0xE, 41);
            emulator.RAM.Store(0xF, 21);

            emulator.Step();
            emulator.Step();
            emulator.Step();
            Assert.AreEqual(62, emulator.Output.Value);
        }

        [Test]
        public void SubstractsTwoNumbersTogetherAndOutputs()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0x1, SUB.OP_CODE + 0xE);
            emulator.RAM.Store(0x2, OUT.OP_CODE);
            emulator.RAM.Store(0xE, 1);
            emulator.RAM.Store(0xF, 0);

            emulator.Step();
            emulator.Step();
            emulator.Step();
            Assert.AreEqual(255, emulator.Output.Value);
        }

        [Test]
        public void ProgramCounterWraps()
        {
            Emulator emulator = new Emulator();

            for (int i = 0; i < 17; i++)
            {
                emulator.Step();
            }

            Assert.AreEqual(1, emulator.programCounter.Value);
        }

        [Test]
        public void CalculatesFibonacciSeries()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDI.OP_CODE + 0);
            emulator.RAM.Store(0x1, STA.OP_CODE + 0xD);
            emulator.RAM.Store(0x2, OUT.OP_CODE);
            emulator.RAM.Store(0x3, LDI.OP_CODE + 1);
            emulator.RAM.Store(0x4, STA.OP_CODE + 0xE);
            emulator.RAM.Store(0x5, OUT.OP_CODE);
            emulator.RAM.Store(0x6, ADD.OP_CODE + 0xD);
            emulator.RAM.Store(0x7, JC.OP_CODE + 0x0);
            emulator.RAM.Store(0x8, STA.OP_CODE + 0xF);
            emulator.RAM.Store(0x9, LDA.OP_CODE + 0xE);
            emulator.RAM.Store(0xA, STA.OP_CODE + 0xD);
            emulator.RAM.Store(0xB, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0xC, JMP.OP_CODE + 0x4);
            emulator.RAM.Store(0xD, 0);
            emulator.RAM.Store(0xE, 0);
            emulator.RAM.Store(0xF, 0);

            List<Byte> values = new List<byte>();

            for (int i = 0; i < 114; i++)
            {
                emulator.Step();

                if ((byte)(emulator.instruction.Value & 0b11110000) == OUT.OP_CODE)
                {
                    values.Add(emulator.Output.Value);
                }
            }

            Assert.AreEqual(14, values.Count);
            Assert.AreEqual(0, values[0]);
            Assert.AreEqual(1, values[1]);
            Assert.AreEqual(1, values[2]);
            Assert.AreEqual(2, values[3]);
            Assert.AreEqual(3, values[4]);
            Assert.AreEqual(5, values[5]);
            Assert.AreEqual(8, values[6]);
            Assert.AreEqual(13, values[7]);
            Assert.AreEqual(21, values[8]);
            Assert.AreEqual(34, values[9]);
            Assert.AreEqual(55, values[10]);
            Assert.AreEqual(89, values[11]);
            Assert.AreEqual(144, values[12]);
            Assert.AreEqual(233, values[13]);
        }

        [Test]
        public void LoadsImmediate()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDI.OP_CODE + 15);

            emulator.Step();

            Assert.AreEqual(15, emulator.A.Value);
        }

        [Test]
        public void StoresToRam()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDI.OP_CODE + 15);
            emulator.RAM.Store(0x1, STA.OP_CODE + 0xF);

            emulator.Step();
            emulator.Step();

            Assert.AreEqual(15, emulator.RAM.Get(0xF));
        }

        [Test]
        public void JumpsWithJCWhenCarryEnabled()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xE);
            emulator.RAM.Store(0x1, ADD.OP_CODE + 0xF);
            emulator.RAM.Store(0x2, JC.OP_CODE + 0x4);
            emulator.RAM.Store(0xE, 255);
            emulator.RAM.Store(0xF, 2);

            emulator.Step();
            emulator.Step();
            emulator.Step();

            Assert.AreEqual(0x4, emulator.programCounter.Value);
        }

        [Test]
        public void DoesNotJumpWithJCWhenCarryDisabled()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xE);
            emulator.RAM.Store(0x1, ADD.OP_CODE + 0xF);
            emulator.RAM.Store(0x2, JC.OP_CODE + 0x4);
            emulator.RAM.Store(0xE, 0);
            emulator.RAM.Store(0xF, 1);

            emulator.Step();
            emulator.Step();
            emulator.Step();

            Assert.AreEqual(0x3, emulator.programCounter.Value);
        }

        [Test]
        public void JumpsWithJZWhenZeroEnabled()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xE);
            emulator.RAM.Store(0x1, ADD.OP_CODE + 0xF);
            emulator.RAM.Store(0x2, JZ.OP_CODE + 0x4);
            emulator.RAM.Store(0xE, 255);
            emulator.RAM.Store(0xF, 1);

            emulator.Step();
            emulator.Step();
            emulator.Step();

            Assert.AreEqual(0x4, emulator.programCounter.Value);
        }

        [Test]
        public void DoesNotJumpWithJZWhenZeroDisabled()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xE);
            emulator.RAM.Store(0x1, ADD.OP_CODE + 0xF);
            emulator.RAM.Store(0x2, JZ.OP_CODE + 0x4);
            emulator.RAM.Store(0xE, 0);
            emulator.RAM.Store(0xF, 1);

            emulator.Step();
            emulator.Step();
            emulator.Step();

            Assert.AreEqual(0x3, emulator.programCounter.Value);
        }

        [Test]
        public void JumpsWithJMP()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, JMP.OP_CODE + 0x4);

            emulator.Step();

            Assert.AreEqual(0x4, emulator.programCounter.Value);
        }

        [Test]
        public void LoadsValueFromRam()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0xF, 255);

            emulator.Step();

            Assert.AreEqual(255, emulator.A.Value);
        }
    }
}
