using Ocl20.library.iface.common;

namespace Ocl20.library.impl.common
{
    public class MultiplicityRangeImpl : MultiplicityRange
    {
        private Multiplicity multiplicity;
        private int lower;
        private int upper;

        public int getLower()
        {
            return lower;
        }

        public void setLower(int newValue)
        {
            lower = newValue;
        }

        public int getUpper()
        {
            return upper;
        }

        public void setUpper(int newValue)
        {
            upper = newValue;
        }

        public Multiplicity getMultiplicity()
        {
            return multiplicity;
        }

        public void setMultiplicity(Multiplicity newValue)
        {
            multiplicity = newValue;
        }
    }
}
