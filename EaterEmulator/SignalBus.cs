using System;
using System.Collections.Generic;
using System.Text;

namespace EaterEmulator
{
    public class SignalBus
    {
        public void Reset()
        {
            HLT = false;
            MI = false;
            RI = false;
            RO = false;
            IO = false;
            II = false;
            AI = false;
            AO = false;
            EO = false;
            SU = false;
            BI = false;
            OI = false;
            CE = false;
            CO = false;
            J = false;
            FI = false;
        }

        public bool HLT { get; set; }

        public bool MI { get; set; }

        public bool RI { get; set; }

        public bool RO { get; set; }

        public bool IO { get; set; }

        public bool II { get; set; }

        public bool AI { get; set; }

        public bool AO { get; set; }

        public bool EO { get; set; }

        public bool SU { get; set; }

        public bool BI { get; set; }

        public bool OI { get; set; }

        public bool CE { get; set; }

        public bool CO { get; set; }

        public bool J { get; set; }

        public bool FI { get; set; }
    }
}
