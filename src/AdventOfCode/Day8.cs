using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day8
    {
        public int Part1()
        {
            var lines = File.ReadAllLines("./input8");

            int accumulator = 0;
            var set = new HashSet<int>();
            int i = 0;
            while (true)
            {
                if (set.Contains(i))
                {
                    return accumulator;
                }

                set.Add(i);

                var cmd = lines[i].Split(' ')[0];
                var arg = lines[i].Split(' ')[1];

                if (cmd == "acc")
                {
                    accumulator += int.Parse(arg);
                    i++;
                }
                else if (cmd == "nop")
                {
                    i++;
                }
                else if (cmd == "jmp")
                {
                    var parsed = int.Parse(arg);
                    i += parsed;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public int Part2()
        {
            var lines = File.ReadAllLines("./input8");

            int attemptNumber = 0;
            var set = new HashSet<int>();

            while (attemptNumber++ < lines.Length)
            {
                int instructionsExecuted = 0;
                int currentLine = 0;
                bool instructionChanged = false;
                int accumulator = 0;
                lines = File.ReadAllLines("./input8");

                while (instructionsExecuted++ < lines.Length)
                {
                    var cmd = lines[currentLine].Split(' ')[0];
                    var arg = lines[currentLine].Split(' ')[1];

                    if (!instructionChanged && !set.Contains(currentLine) && (cmd == "nop" || cmd == "jmp"))
                    {
                        cmd = cmd switch
                        {
                            "nop" => "jmp",
                            "jmp" => "nop",
                            _ => throw new ArgumentException()
                        };

                        lines[currentLine] = $"{cmd} {arg}";
                        instructionChanged = true;
                        set.Add(currentLine);
                    }

                    switch (cmd)
                    {
                        case "acc":
                            accumulator += int.Parse(arg);
                            currentLine++;
                            break;
                        case "nop":
                            currentLine++;
                            break;
                        case "jmp":
                        {
                            var parsed = int.Parse(arg);
                            currentLine += parsed;
                            break;
                        }
                        default:
                            throw new ArgumentException();
                    }

                    if (currentLine >= lines.Length)
                    {
                        return accumulator;
                    }
                }
            }

            return -1;
        }
    }
}