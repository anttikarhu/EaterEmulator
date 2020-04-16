using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EaterEmulator.Registers
{
    public abstract class Register : IBusConnectedModule, IResetableModule, INotifyPropertyChanged
    {
        public DataBus Bus { get; internal set; }

        public SignalBus Signals { get; internal set; }

        public Register(DataBus bus, SignalBus signals)
        {
            this.Bus = bus;
            this.Signals = signals;
        }

        private byte value;

        public virtual byte Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void ReadFromBus() { }

        public virtual void WriteToBus() { }

        public void Reset()
        {
            Value = 0;
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
