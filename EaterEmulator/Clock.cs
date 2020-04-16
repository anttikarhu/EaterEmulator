using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace EaterEmulator
{
    public class Clock
    {
        public int Speed { get; set; } = 50;

        private readonly Timer timer;

        public event EventHandler? RisingEdge;

        public event EventHandler? FallingEdge;

        private bool rising = false;

        private bool isHalted = false;

        public bool IsHalted { 
            get
            {
                return isHalted;
            }
            set
            {
                isHalted = value;

                if (isHalted)
                {
                    Stop();
                }
            }
        }

        public Clock()
        {
            timer = new Timer(Speed);
            timer.Elapsed += TimerElapsed;
            timer.Enabled = false;
        }

        public void Start()
        {
            if (!IsHalted && !timer.Enabled)
            {
                timer.Enabled = true;
                TimerElapsed(null, null);
            }
        }

        public void Stop()
        {
            timer.Enabled = false;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs? e)
        {
            rising = !rising;

            if (rising)
            {
                RisingEdge?.Invoke(this, new EventArgs());
            }
            else
            {
                FallingEdge?.Invoke(this, new EventArgs());
            }
        }
    }
}
