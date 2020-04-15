namespace EaterEmulator
{
    public class InstructionCounter
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
    }
}
