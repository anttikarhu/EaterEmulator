﻿using EaterEmulator.Operations;
using EaterEmulator.Registers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator
{
    public class Emulator
    {
        private readonly DataBus bus = new DataBus();

        public readonly FlagBus flagBus = new FlagBus();

        public readonly SignalBus signals = new SignalBus();

        public readonly Register A;

        private readonly Register B;

        private readonly SumRegister sum;

        public readonly Register instruction;

        public readonly FlagsRegister flags;

        public readonly Register output;

        private readonly Register memoryAddress;

        public readonly Memory RAM;

        public ProgramCounter ProgramCounter { get; private set; }

        public byte InstructionCounter { get; private set; }

        public bool IsHalted { get; set; }

        private readonly Dictionary<byte, Operation> operations = new Dictionary<byte, Operation>();

        public Emulator()
        {
            this.ProgramCounter = new ProgramCounter(this.bus, this.signals);

            this.A = new ARegister(bus, signals);
            this.B = new BRegister(bus, signals);
            this.sum = new SumRegister(A, B, bus, signals, flagBus);
            this.instruction = new InstructionRegister(bus, signals);
            this.flags = new FlagsRegister(bus, signals, flagBus);
            this.output = new OutputRegister(bus, signals);
            this.memoryAddress = new MemoryAddressRegister(bus, signals);
            this.RAM = new Memory(this.bus, this.signals, this.memoryAddress);

            operations.Add(NOP.OP_CODE, new NOP(this, signals, flags));
            operations.Add(LDA.OP_CODE, new LDA(this, signals, flags));
            operations.Add(ADD.OP_CODE, new ADD(this, signals, flags));
            operations.Add(SUB.OP_CODE, new SUB(this, signals, flags));
            operations.Add(STA.OP_CODE, new STA(this, signals, flags));
            operations.Add(LDI.OP_CODE, new LDI(this, signals, flags));
            operations.Add(JMP.OP_CODE, new JMP(this, signals, flags));
            operations.Add(JC.OP_CODE, new JC(this, signals, flags));
            operations.Add(JZ.OP_CODE, new JZ(this, signals, flags));
            operations.Add(0b10010000, new NOP(this, signals, flags));
            operations.Add(0b10100000, new NOP(this, signals, flags));
            operations.Add(0b10110000, new NOP(this, signals, flags));
            operations.Add(0b11000000, new NOP(this, signals, flags));
            operations.Add(0b11010000, new NOP(this, signals, flags));
            operations.Add(OUT.OP_CODE, new OUT(this, signals, flags));
            operations.Add(HLT.OP_CODE, new HLT(this, signals, flags));
        }

        public void Clk()
        {
            if (IsHalted)
            {
                return;
            }

            signals.Reset();

            Operation operation = GetOperation(instruction.Value);

            operation.Clk();

            InstructionCounter++;
            if (InstructionCounter == 5)
            {
                InstructionCounter = 0;
            }
       
            RAM.WriteToBus();
            instruction.WriteToBus();
            ProgramCounter.WriteToBus();
            memoryAddress.WriteToBus();
            sum.WriteToBus();
            A.WriteToBus();
            B.WriteToBus();
            flags.WriteToBus();
            output.WriteToBus();

            RAM.ReadFromBus();
            instruction.ReadFromBus();
            ProgramCounter.ReadFromBus();
            memoryAddress.ReadFromBus();
            sum.ReadFromBus();
            A.ReadFromBus();
            B.ReadFromBus();
            flags.ReadFromBus();
            output.ReadFromBus();

            if (signals.HLT)
            {
                IsHalted = true;
            }
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
