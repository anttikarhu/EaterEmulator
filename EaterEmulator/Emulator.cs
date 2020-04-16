using EaterEmulator.Operations;
using EaterEmulator.Registers;
using System.Collections.Generic;

namespace EaterEmulator
{
    public class Emulator
    {
        public Clock Clock { get; internal set; }

        private readonly DataBus bus = new DataBus();

        private readonly FlagBus flagBus = new FlagBus();

        private readonly SignalBus signals = new SignalBus();

        public Register A { get; internal set; }

        public Register B { get; internal set; }

        public SumRegister Sum { get; internal set; }

        public Register Instruction { get; internal set; }

        public FlagsRegister Flags { get; internal set; }

        public Register Output { get; internal set; }

        public Register MemoryAddress { get; internal set; }

        public Memory RAM { get; internal set; }

        public ProgramCounter ProgramCounter { get; internal set; }

        public InstructionCounter InstructionCounter { get; internal set; }

        private readonly Dictionary<byte, Operation> operations = new Dictionary<byte, Operation>();

        public Emulator()
        {
            this.Clock = new Clock();
            this.Clock.RisingEdge += OnRisingEdge;

            this.ProgramCounter = new ProgramCounter(this.bus, this.signals);
            this.InstructionCounter = new InstructionCounter();

            this.A = new ARegister(bus, signals);
            this.B = new BRegister(bus, signals);
            this.Sum = new SumRegister(A, B, bus, signals, flagBus);
            this.Instruction = new InstructionRegister(bus, signals);
            this.Flags = new FlagsRegister(bus, signals, flagBus);
            this.Output = new OutputRegister(bus, signals);
            this.MemoryAddress = new MemoryAddressRegister(bus, signals);
            this.RAM = new Memory(this.bus, this.signals, this.MemoryAddress);

            operations.Add(NOP.OP_CODE, new NOP(InstructionCounter, signals, Flags));
            operations.Add(LDA.OP_CODE, new LDA(InstructionCounter, signals, Flags));
            operations.Add(ADD.OP_CODE, new ADD(InstructionCounter, signals, Flags));
            operations.Add(SUB.OP_CODE, new SUB(InstructionCounter, signals, Flags));
            operations.Add(STA.OP_CODE, new STA(InstructionCounter, signals, Flags));
            operations.Add(LDI.OP_CODE, new LDI(InstructionCounter, signals, Flags));
            operations.Add(JMP.OP_CODE, new JMP(InstructionCounter, signals, Flags));
            operations.Add(JC.OP_CODE, new JC(InstructionCounter, signals, Flags));
            operations.Add(JZ.OP_CODE, new JZ(InstructionCounter, signals, Flags));
            operations.Add(0b10010000, new NOP(InstructionCounter, signals, Flags));
            operations.Add(0b10100000, new NOP(InstructionCounter, signals, Flags));
            operations.Add(0b10110000, new NOP(InstructionCounter, signals, Flags));
            operations.Add(0b11000000, new NOP(InstructionCounter, signals, Flags));
            operations.Add(0b11010000, new NOP(InstructionCounter, signals, Flags));
            operations.Add(OUT.OP_CODE, new OUT(InstructionCounter, signals, Flags));
            operations.Add(HLT.OP_CODE, new HLT(InstructionCounter, signals, Flags));
        }

        private void OnRisingEdge(object? sender, System.EventArgs e)
        {
            Clk();
        }

        public void Clk()
        {
            if (Clock.IsHalted)
            {
                return;
            }

            signals.Reset();

            Operation operation = GetOperation(Instruction.Value);

            operation.Clk();
            InstructionCounter.Clk();

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

            if (signals.HLT)
            {
                Clock.IsHalted = true;
            }
        }

        public void Step()
        {
            Clk();
            Clk();
            Clk();
            Clk();
            Clk();
        }

        public void Reset()
        {
            InstructionCounter.Reset();
            ProgramCounter.Reset();
            A.Reset();
            B.Reset();
            Flags.Reset();
            Instruction.Reset();
            MemoryAddress.Reset();
            Output.Reset();
            Sum.Reset();
        }

        public Operation GetOperation(byte instructionRegisterValue)
        {
            byte opCode = (byte)(instructionRegisterValue & 0b11110000);
            return operations[opCode];
        }
    }
}
