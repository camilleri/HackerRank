using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappinessScore
{
    class Solution
    {
        static HashSet<uint> generatePrimeNumbers(uint max)
        {
            HashSet<uint> res = new HashSet<uint>();
            for (uint i = 2; i <= max; i++)
            {
                bool prime = true;
                for (uint j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                        prime = false;
                }
                if (prime)
                    res.Add(i);
            }

            return res;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            string[] line = Console.ReadLine().Split();

            uint[] scores = new uint[N];
            uint max = 0;
            for (int i = 0; i < N; ++i)
            {
                scores[i] = uint.Parse(line[i]);
                max += scores[i];
            }

            HashSet<uint> primes = generatePrimeNumbers(max);

            HashSet<uint> sol = new HashSet<uint>();
            int num_comb = (int)Math.Pow(2, N);
            for (int comb = 1; comb < num_comb; ++comb)
            {
                uint sum = 0;
                for (int i = 0; i < N; ++i)
                {
                    int num = 1 << i;
                    if ((comb & num) != 0)
                        sum += scores[i];
                }

                if (primes.Contains(sum))
                    sol.Add(sum);
            }


            Console.WriteLine(sol.Count());
        }
    }
}
