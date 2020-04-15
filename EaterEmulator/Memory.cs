using EaterEmulator.Registers;

namespace EaterEmulator
{
    public class Memory
    {
        private byte[] data = new byte[16];

        private DataBus bus;

        private SignalBus signals;

        private Register memoryAddressRegister;

        public Memory(DataBus bus, SignalBus signals, Register memoryAddressRegister)
        {
            this.bus = bus;
            this.signals = signals;
            this.memoryAddressRegister = memoryAddressRegister;
        }

        public byte Get(byte address)
        {
            return data[address];
        }

        public void Store(byte address, byte value)
        {
            data[address] = value;
        }

        public void ReadFromBus()
        {
            if (signals.RI)
            {
                data[memoryAddressRegister.Value] = bus.Value;
            }
        }

        public void WriteToBus()
        {
            if (signals.RO)
            {
                bus.Value = data[memoryAddressRegister.Value];
            }
        }
    }
}
