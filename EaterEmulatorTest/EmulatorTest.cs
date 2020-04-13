using EaterEmulator;
using EaterEmulator.Operations;
using EaterEmulator.Registers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Tests
{
    public class EmulatorTest
    {
        [Test]
        public void StepGetsTheNextInstructionFromRAM()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, 0b00010000);

            emulator.Step();

            Assert.AreEqual(LDA.OP_CODE, emulator.Instruction.Value);
            Assert.AreEqual(1, emulator.ProgramCounter);
        }

        [Test]
        public void OutputsNumberAndHaltsByStepping()
        {
            Emulator emulator = new Emulator();
            emulator.RAM.Store(0x0, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0x1, OUT.OP_CODE);
            emulator.RAM.Store(0x2, HLT.OP_CODE);
            emulator.RAM.Store(0xF, 62);

            emulator.Step();
            emulator.Step();
            emulator.Step();

            Assert.IsTrue(emulator.IsHalted);
            Assert.AreEqual(3, emulator.ProgramCounter);
            Assert.AreEqual(62, emulator.Output.Value);
        }

        [Test]
        public void DoesNotRunWhenHalted()
        {
            Emulator emulator = new Emulator();
            emulator.ProgramCounter = 0;
            emulator.IsHalted = true;

            emulator.Step();

            Assert.AreEqual(0, emulator.ProgramCounter);
        }

        [Test]
        public void TestFlags()
        {
            FlagsRegister flagsRegister = new FlagsRegister(null, null);
            flagsRegister.Value = 0;

            Assert.IsFalse(flagsRegister.Zero);
            Assert.IsFalse(flagsRegister.Carry);

            flagsRegister.Value = 3;

            Assert.IsTrue(flagsRegister.Zero);
            Assert.IsTrue(flagsRegister.Carry);
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
        public void ProgramCounterWraps()
        {
            Emulator emulator = new Emulator();
            
            for(int i = 0; i < 17; i++)
            {
                emulator.Step();
            }

            Assert.AreEqual(1, emulator.ProgramCounter);
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
            emulator.RAM.Store(0x7, JC.OP_CODE  + 0x0);
            emulator.RAM.Store(0x8, STA.OP_CODE + 0xF);
            emulator.RAM.Store(0x9, LDA.OP_CODE + 0xE);
            emulator.RAM.Store(0xA, STA.OP_CODE + 0xD);
            emulator.RAM.Store(0xB, LDA.OP_CODE + 0xF);
            emulator.RAM.Store(0xC, JMP.OP_CODE + 0x4);
            emulator.RAM.Store(0xD, 0);
            emulator.RAM.Store(0xE, 0);
            emulator.RAM.Store(0xF, 0);

            List<Byte> values = new List<byte>();

            for(int i = 0; i < 114; i++)
            {
                emulator.Step();

                if ((byte)(emulator.Instruction.Value & 0b11110000) == OUT.OP_CODE)
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
    }
}
