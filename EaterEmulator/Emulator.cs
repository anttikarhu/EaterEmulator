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

        public FlagBus FlagBus { get; }

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
            this.FlagBus = new FlagBus();
            this.Signals = new SignalBus();
            this.ProgramCounter = new ProgramCounter(this.Bus, this.Signals);

            this.A = new ARegister(Bus, Signals);
            this.B = new BRegister(Bus, Signals);
            this.Sum = new SumRegister(A, B, Bus, Signals, FlagBus);
            this.Instruction = new InstructionRegister(Bus, Signals);
            this.Flags = new FlagsRegister(Bus, Signals, FlagBus);
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
            operations.Add(0b10010000, new NOP(this));
            operations.Add(0b10100000, new NOP(this));
            operations.Add(0b10110000, new NOP(this));
            operations.Add(0b11000000, new NOP(this));
            operations.Add(0b11010000, new NOP(this));
            operations.Add(OUT.OP_CODE, new OUT(this));
            operations.Add(HLT.OP_CODE, new HLT(this));
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
       
            RAM.WriteToBus();
            Instruction.WriteToBus();
            ProgramCounter.WriteToBus();
            MemoryAddress.WriteToBus();
            Sum.WriteToBus();
            A.WriteToBus();
            B.WriteToBus();
            Flags.WriteToBus();
            Output.WriteToBus();

            RAM.ReadFromBus();
            Instruction.ReadFromBus();
            ProgramCounter.ReadFromBus();
            MemoryAddress.ReadFromBus();
            Sum.ReadFromBus();
            A.ReadFromBus();
            B.ReadFromBus();
            Flags.ReadFromBus();
            Output.ReadFromBus();
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
