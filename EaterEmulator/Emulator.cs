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

        private readonly FlagBus flagBus = new FlagBus();

        public readonly SignalBus signals = new SignalBus();

        public Register A { get; }

        private readonly Register B;

        private readonly SumRegister sum;

        public Register Instruction { get; }

        public FlagsRegister Flags { get; }

        public Register Output { get; }

        private readonly Register memoryAddress;

        public Memory RAM { get; }

        public ProgramCounter ProgramCounter { get; private set; }

        public byte InstructionCounter { get; private set; }

        public bool IsHalted { get; set; }

        private Dictionary<byte, Operation> operations = new Dictionary<byte, Operation>();

        public Emulator()
        {
            this.ProgramCounter = new ProgramCounter(this.bus, this.signals);

            this.A = new ARegister(bus, signals);
            this.B = new BRegister(bus, signals);
            this.sum = new SumRegister(A, B, bus, signals, flagBus);
            this.Instruction = new InstructionRegister(bus, signals);
            this.Flags = new FlagsRegister(bus, signals, flagBus);
            this.Output = new OutputRegister(bus, signals);
            this.memoryAddress = new MemoryAddressRegister(bus, signals);
            this.RAM = new Memory(this.bus, this.signals, this.memoryAddress);

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

            signals.Reset();

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
            memoryAddress.WriteToBus();
            sum.WriteToBus();
            A.WriteToBus();
            B.WriteToBus();
            Flags.WriteToBus();
            Output.WriteToBus();

            RAM.ReadFromBus();
            Instruction.ReadFromBus();
            ProgramCounter.ReadFromBus();
            memoryAddress.ReadFromBus();
            sum.ReadFromBus();
            A.ReadFromBus();
            B.ReadFromBus();
            Flags.ReadFromBus();
            Output.ReadFromBus();

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
