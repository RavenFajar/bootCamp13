namespace PrimeService
{
    public class PrimeService
    {
        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
                return false;

            if (candidate == 2)
                return true;

            if (candidate % 2 == 0)
                return false;

            var boundary = (int)Math.Floor(Math.Sqrt(candidate));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (candidate % i == 0)
                    return false;
            }

            return true;
        }

        public int[] FindPrimesUpTo(int limit)
        {
            throw new NotImplementedException("Method not implemented yet");
        }

        public int GetNextPrime(int number)
        {
            throw new NotImplementedException("Method not implemented yet");
        }
    }
}