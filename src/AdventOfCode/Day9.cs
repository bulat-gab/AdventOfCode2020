using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day9
    {
        public long Part1()
        {
            var numbers = Parse();

            var preamble = 25;
            
            for (int i = preamble; i < numbers.Length; i++)
            {
                var dict = new Dictionary<long, int>();
                var complementFound = false;
                for (int j = i - preamble; j < i; j++)
                {
                    long complement = numbers[i] - numbers[j];
                    if (dict.TryGetValue(complement, out _))
                    {
                        complementFound = true;
                        break;
                    }
                    dict.Add(numbers[j], j);
                }

                if (!complementFound)
                {
                    return numbers[i];
                }
            }

            return -1;
        }

        public long Part2()
        {
            var numbers = Parse();
            long target = Part1();

            int i = 0;
            int j = 0;
            long sum = 0;
            
            while (sum != target && j < numbers.Length)
            {
                sum += numbers[j];
                j++;
                while (sum > target)
                {
                    sum -= numbers[i];
                    i++;
                }

                if (i == j && sum == target)
                {
                    i++;
                    j++;
                    sum = 0;
                }
            }

            var min = long.MaxValue;
            var max = long.MinValue;
            for (int k = i; k <= j; k++)
            {
                min = Math.Min(min, numbers[k]);
                max = Math.Max(max, numbers[k]);
            }

            return min + max;
        }

        private long[] Parse() => File.ReadAllLines("./input9").Select(long.Parse).ToArray();
    }
}