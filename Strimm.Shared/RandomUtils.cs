using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Shared
{
    public static class RandomUtils
    {
        public static int RandomNumber(int min, int max)
        {
            int number = (new Random()).Next(min, max);

            return number;
        }

        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            char ch;
            string randomString;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            randomString = lowerCase ? builder.ToString().ToLower() : builder.ToString();

            return randomString;
        }

        public static int[] GenerateArrayOfRandomNumbersInRange(int min, int max, int size)
        {
            Random random = new Random();
            var listOfRandomNumbers = new List<int>();

            for (int i=0; i < size; i++)
            {
                int value = random.Next(min, max);
                if (!listOfRandomNumbers.Contains(value))
                {
                    listOfRandomNumbers.Add(value);
                }
                else
                {
                    i--;
                }
            }

            return listOfRandomNumbers.ToArray();
        }
    }
}
