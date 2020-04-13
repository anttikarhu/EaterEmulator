using EaterEmulator.Operations;
using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator
{
    public class Emulator
    {
        public DataBus Bus { get; }

        public SignalBus Signals { get; }

        public Register A { get; }

        public Register B { get; }

        public SumRegister Sum { get; }

        public Register Instruction { get; }

        public FlagsRegister Flags { get; }

        public Register Output { get; }

        public Register MemoryAddress { get; }

        public Memory RAM { get; }

        public ProgramCounter ProgramCounter { get; set; }

        public byte InstructionCounter { get; set; }

        public bool IsHalted { get; set; }

        private Dictionary<byte, Operation> operations = new Dictionary<byte, Operation>();

        public Emulator()
        {
            this.Bus = new DataBus();
            this.Signals = new SignalBus();
            this.ProgramCounter = new ProgramCounter(this.Bus, this.Signals);

            this.A = new ARegister(Bus, Signals);
            this.B = new BRegister(Bus, Signals);
            this.Sum = new SumRegister(A, B, Bus, Signals);
            this.Instruction = new InstructionRegister(Bus, Signals);
            this.Flags = new FlagsRegister(Bus, Signals);
            this.Output = new OutputRegister(Bus, Signals);
            this.MemoryAddress = new MemoryAddressRegister(Bus, Signals);
            this.RAM = new Memory(this.Bus, this.Signals, this.MemoryAddress);

            operations.Add(NOP.OP_CODE, new NOP(this));
            operations.Add(LDA.OP_CODE, new LDA(this));
            operations.Add(ADD.OP_CODE, new ADD(this));
            operations.Add(SUB.OP_CODE, new SUB(this));
            operations.Add(STA.OP_CODE, new STA(this));
            operations.Add(LDI.OP_CODE, new LDI(this));
            operations.Add(JMP.OP_CODE, new JMP(this));
            operations.Add(JC.OP_CODE, new JC(this));
            operations.Add(JZ.OP_CODE, new JZ(this));
            operations.Add(BEQ.OP_CODE, new BEQ(this));
            operations.Add(BNE.OP_CODE, new BNE(this));
            operations.Add(0b10110000, new NOP(this));
            operations.Add(0b11000000, new NOP(this));
            operations.Add(0b11010000, new NOP(this));
            operations.Add(OUT.OP_CODE, new OUT(this));
            operations.Add(HLT.OP_CODE, new HLT(this));
        }

        public void Step()
        {
            if (IsHalted)
            {
                return;
            }

            // Get next instruction from memory and increment program counter
            Instruction.Value = RAM.Get(ProgramCounter.Value);
            ProgramCounter.Value++;

            if (ProgramCounter.Value >= 16)
            {
                ProgramCounter.Value = 0;
            }

            Operation operation = GetOperation(Instruction.Value);
            byte operand = (byte)(Instruction.Value & 0b00001111);
            operation.Run(operand);
        }

        public void Clk()
        {
            if (IsHalted)
            {
                return;
            }

            Signals.Reset();

            Operation operation = GetOperation(Instruction.Value);

            operation.Clk();

            InstructionCounter++;
            if (InstructionCounter == 5)
            {
                InstructionCounter = 0;
            }

            ProgramCounter.Clk();
            RAM.Clk();
            Instruction.Clk();
            MemoryAddress.Clk();
            Sum.Clk();
            A.Clk();
            B.Clk();
            Flags.Clk();
            Output.Clk();
        }

        public void ClkX5()
        {
            Clk();
            Clk();
            Clk();
            Clk();
            Clk();
        }

        public Operation GetOperation(byte instructionRegisterValue)
        {
            byte opCode = (byte) (instructionRegisterValue & 0b11110000);
            return operations[opCode];
        }
    }
}
