using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day2
{
    public class Day2
    {
        private readonly string[] _lines = File.ReadAllLines("./Day2/input2");

        public int Part1()
        {
            var validPasswords = 0;
            foreach (var line in _lines)
            {
                var (password, givenLetter, min, max) = GetValues(line);
            
                var givenLetterCount = password.Count(ch => ch == givenLetter);
            
                if (givenLetterCount >= min && givenLetterCount <= max)
                    validPasswords++;
            }
            
            
            return validPasswords;
        }
        
        public int Part2()
        {
            var validPasswords = 0;
            foreach (var line in _lines)
            {
                var (password, givenLetter, firstPos, secondPos) = GetValues(line);

                if (firstPos > password.Length || secondPos > password.Length)
                    continue;

                var charIsAtFirstPos = password[firstPos - 1] == givenLetter;
                var charIsAtSecondPos = password[secondPos - 1] == givenLetter;

                if (charIsAtFirstPos != charIsAtSecondPos)
                    validPasswords++;
            }
            
            
            return validPasswords;
        }

        private (string, char, int, int) GetValues(string line)
        {
            var password = line.Split(':')[1].Substring(1);
            
            var policy = line.Split(':')[0];
            var givenLetter = policy.Split(' ')[1][0];
        
            var range = policy.Split(' ')[0];
            var min = int.Parse(range.Split('-')[0]);
            var max = int.Parse(range.Split('-')[1]);

            return (password, givenLetter, min, max);
        }
    }
}