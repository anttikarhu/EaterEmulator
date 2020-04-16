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

        public readonly Register A;

        private readonly Register B;

        private readonly SumRegister sum;

        public readonly Register instruction;

        private readonly FlagsRegister flags;

        public Register Output { get; set; }

        private readonly Register memoryAddress;

        public readonly Memory RAM;

        public readonly ProgramCounter programCounter;

        private readonly InstructionCounter instructionCounter = new InstructionCounter();

        private readonly Dictionary<byte, Operation> operations = new Dictionary<byte, Operation>();

        public Emulator()
        {
            this.Clock = new Clock();
            this.Clock.RisingEdge += OnRisingEdge;

            this.programCounter = new ProgramCounter(this.bus, this.signals);

            this.A = new ARegister(bus, signals);
            this.B = new BRegister(bus, signals);
            this.sum = new SumRegister(A, B, bus, signals, flagBus);
            this.instruction = new InstructionRegister(bus, signals);
            this.flags = new FlagsRegister(bus, signals, flagBus);
            this.Output = new OutputRegister(bus, signals);
            this.memoryAddress = new MemoryAddressRegister(bus, signals);
            this.RAM = new Memory(this.bus, this.signals, this.memoryAddress);

            operations.Add(NOP.OP_CODE, new NOP(instructionCounter, signals, flags));
            operations.Add(LDA.OP_CODE, new LDA(instructionCounter, signals, flags));
            operations.Add(ADD.OP_CODE, new ADD(instructionCounter, signals, flags));
            operations.Add(SUB.OP_CODE, new SUB(instructionCounter, signals, flags));
            operations.Add(STA.OP_CODE, new STA(instructionCounter, signals, flags));
            operations.Add(LDI.OP_CODE, new LDI(instructionCounter, signals, flags));
            operations.Add(JMP.OP_CODE, new JMP(instructionCounter, signals, flags));
            operations.Add(JC.OP_CODE, new JC(instructionCounter, signals, flags));
            operations.Add(JZ.OP_CODE, new JZ(instructionCounter, signals, flags));
            operations.Add(0b10010000, new NOP(instructionCounter, signals, flags));
            operations.Add(0b10100000, new NOP(instructionCounter, signals, flags));
            operations.Add(0b10110000, new NOP(instructionCounter, signals, flags));
            operations.Add(0b11000000, new NOP(instructionCounter, signals, flags));
            operations.Add(0b11010000, new NOP(instructionCounter, signals, flags));
            operations.Add(OUT.OP_CODE, new OUT(instructionCounter, signals, flags));
            operations.Add(HLT.OP_CODE, new HLT(instructionCounter, signals, flags));
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

            Operation operation = GetOperation(instruction.Value);

            operation.Clk();
            instructionCounter.Clk();

            RAM.WriteToBus();
            instruction.WriteToBus();
            programCounter.WriteToBus();
            memoryAddress.WriteToBus();
            sum.WriteToBus();
            A.WriteToBus();
            B.WriteToBus();
            flags.WriteToBus();
            Output.WriteToBus();

            RAM.ReadFromBus();
            instruction.ReadFromBus();
            programCounter.ReadFromBus();
            memoryAddress.ReadFromBus();
            sum.ReadFromBus();
            A.ReadFromBus();
            B.ReadFromBus();
            flags.ReadFromBus();
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
            instructionCounter.Reset();
            programCounter.Reset();
            A.Reset();
            B.Reset();
            flags.Reset();
            instruction.Reset();
            memoryAddress.Reset();
            Output.Reset();
            sum.Reset();
        }

        public Operation GetOperation(byte instructionRegisterValue)
        {
            byte opCode = (byte)(instructionRegisterValue & 0b11110000);
            return operations[opCode];
        }
    }
}
