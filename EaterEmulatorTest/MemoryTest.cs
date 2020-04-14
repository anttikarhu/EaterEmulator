using EaterEmulator.Registers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator.Tests
{
    public class MemoryTest
    {
        [Test]
        public void MemoryInputsDataToAddressDefinedInRegister()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register memoryAddress = new MemoryAddressRegister(bus, signals);
            Memory ram = new Memory(bus, signals, memoryAddress);

            bus.Value = 255;
            memoryAddress.Value = 0xF;
            signals.RI = true;

            ram.ReadFromBus();

            Assert.AreEqual(255, ram.Get(0xF));
        }

        [Test]
        public void MemoryOutputsDataByAddressInRegister()
        {
            DataBus bus = new DataBus();
            SignalBus signals = new SignalBus();

            Register memoryAddress = new MemoryAddressRegister(bus, signals);
            Memory ram = new Memory(bus, signals, memoryAddress);

            ram.Store(0xF, 255);
            memoryAddress.Value = 0xF;
            signals.RO = true;

            ram.WriteToBus();

            Assert.AreEqual(255, bus.Value);
        }
    }
}
