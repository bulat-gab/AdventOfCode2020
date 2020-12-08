using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day6
    {
        public int Part1()
        {
            var input = File.ReadAllText("./input6");

            var groups = input.Split("\r\n\r\n");

            var sum = 0;
            foreach (var group in groups)
            {
                var set = new HashSet<char>();

                var people = group.Split("\r\n");
                foreach (var p in people)
                {
                    foreach (var ch in p.ToCharArray())
                    {
                        set.Add(ch);
                    }
                }
                sum += set.Count;
            }
            return sum;
        }

        public int Part2()
        {
            var input = File.ReadAllText("./input6");
            var groups = input.Split("\r\n\r\n");
            var sum = 0;
            
            foreach (var group in groups)
            {
                var people = group.Split("\r\n");
                if (people.Length == 1)
                {
                    sum += people[0].Length;
                    continue;
                }

                var answersOfFirstPerson = people[0].ToCharArray();
                foreach (var ch in answersOfFirstPerson)
                {
                    if (people.Skip(1).All(x => x.Contains(ch)))
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }
    }
}