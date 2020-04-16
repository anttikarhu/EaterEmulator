using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator
{
    public interface IBusConnectedModule
    {
        public void ReadFromBus();

        public void WriteToBus();
    }
}
