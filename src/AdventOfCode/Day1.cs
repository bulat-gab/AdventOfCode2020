using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day1
    {
        private readonly int[] _lines = File.ReadAllLines("./input1")
            .Select(int.Parse)
            .ToArray();
        
        // Space: O(N); Time: O(N)
        public int Part1()
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < _lines.Length; i++)
            {
                dict[_lines[i]] = i;
            }

            for (int i = 0; i < _lines.Length; i++)
            {
                var y = 2020 - _lines[i];
                if (dict.ContainsKey(y))
                {
                    return _lines[i] * y;
                }
            }

            return 0;
        }

        // Space: O(N); Time: O(N^2)
        public int Part2()
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < _lines.Length; i++)
            {
                dict[_lines[i]] = i;
            }
            
            for (int i = 0; i < _lines.Length - 1; i++)
            {
                for (int j = i + 1; j < _lines.Length; j++)
                {
                    var x = _lines[i];
                    var y = _lines[j];
                    var z = 2020 - x - y;
                    if (dict.ContainsKey(z))
                    {
                        return x * y * z;
                    }
                }
            }

            return 0;
        }
    }
}