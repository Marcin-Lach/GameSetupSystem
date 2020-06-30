namespace ThirdApproachDomain
{
    public class ZeroPositiveInt
    {
        public int Value { get; }

        public ZeroPositiveInt(int value)
        {
            if (value < 0)
            {
                throw new BusinessLogicException($"ZeroPositiveInt cannot be less than 0. Passed value {value}");
            }

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ZeroPositiveInt castObj)
            {
                return castObj.Equals(this);
            }

            return false;
        }

        protected bool Equals(ZeroPositiveInt other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}