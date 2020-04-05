using EaterEmulator.Operations;
using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator
{
    public class Emulator
    {
        public Register A { get; }

        public Register B { get; }

        public SumRegister Sum { get; }

        public Register Instruction { get; }

        public FlagsRegister Flags { get; }

        public Register Output { get; }

        public Memory RAM { get; }

        public byte ProgramCounter { get; set; }

        public bool IsHalted { get; set; }

        public Emulator()
        {
            this.A = new Register();
            this.B = new Register();
            this.Sum = new SumRegister(this);
            this.Instruction = new Register();
            this.Flags = new FlagsRegister();
            this.Output = new Register();
            this.RAM = new Memory();
        }

        public void Step()
        {
            if (IsHalted)
            {
                return;
            }

            // Get next instruction from memory and increment program counter
            Instruction.Value = RAM.Get(ProgramCounter);
            ProgramCounter++;

            if (ProgramCounter >= 16)
            {
                ProgramCounter = 0;
            }

            Operation operation = GetOperation(Instruction.Value);
            byte operand = (byte)(Instruction.Value & 0b00001111);
            operation.Run(operand);
        }

        public Operation GetOperation(byte instructionRegisterValue)
        {
            byte opCode = (byte) (instructionRegisterValue & 0b11110000);

            if (opCode == LDA.OP_CODE)
            {
                return new LDA(this);
            } 
            else if (opCode == ADD.OP_CODE)
            {
                return new ADD(this);
            }
            else if (opCode == OUT.OP_CODE)
            {
                return new OUT(this);
            } 
            else if (opCode == HLT.OP_CODE)
            {
                return new HLT(this);
            }
            else if (opCode == SUB.OP_CODE)
            {
                return new SUB(this);
            }
            else if (opCode == STA.OP_CODE)
            {
                return new STA(this);
            }
            else if (opCode == LDI.OP_CODE)
            {
                return new LDI(this);
            }
            else if (opCode == JMP.OP_CODE)
            {
                return new JMP(this);
            }
            else if (opCode == JC.OP_CODE)
            {
                return new JC(this);
            }
            else if (opCode == JZ.OP_CODE)
            {
                return new JZ(this);
            }
            else
            {
                return new NOP(this);
            }
        }
    }
}
