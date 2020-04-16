namespace EaterEmulator
{
    public class InstructionCounter : IResetableModule
    {
        public byte Value { get; private set; }

        public void Clk()
        {
            Value++;

            if (Value == 5)
            {
                Value = 0;
            }
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}
