using System;
using System.IO;

namespace AdventOfCode
{
    public class Day5
    {
        public int Part1()
        {
            var lines = File.ReadAllLines("./input5");

            var max = 0;
            foreach (var line in lines)
            {
                var id = GetId(line);
                max = Math.Max(max, id);
            }

            return max;
        }

        private int GetId(string line)
        {
            var upper = 127;
            var lower = 0;
            
            foreach (var ch in line.Substring(0, 7))
            {
                var range = upper - lower;
                if (ch == 'F')
                {
                    upper -= range / 2;
                }
                else if (ch == 'B')
                {
                    lower += range / 2;
                }
                else
                {
                    throw new ArgumentException(nameof(line));
                }
            }

            var row = line[6] == 'F' ? upper : lower;

            upper = 7;
            lower = 0;
            foreach (var ch in line.Substring(7))
            {
                var range = upper - lower;
                if (ch == 'L')
                {
                    upper -= range / 2;
                }
                else if (ch == 'R')
                {
                    lower += range / 2;
                }
                else
                {
                    throw new ArgumentException(nameof(line));
                }
            }
            
            var col = line[9] == 'L' ? upper : lower;

            return row * 8 + col;
        }
    }
}