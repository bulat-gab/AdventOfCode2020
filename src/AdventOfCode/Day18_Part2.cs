using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public partial class Day18
    {
        public long Part2()
        {
            long sum = 0;
            foreach (var line in lines)
            {
                sum += EvaluateForPart2(line);
            }
            
            return sum;
        }

        public long EvaluateForPart2(string line)
        {
            var i = 0;
            var operations = new Stack<char>();
            var numbers = new Stack<long>();
            
            while (i < line.Length)
            {
                var ch = line[i];

                if (ch == ' ')
                {
                    i++;
                    continue;
                }
                else if (ch == '+' || ch == '*' || ch == '(')
                {
                    operations.Push(ch);
                }
                else if (int.TryParse(ch.ToString(), out var number))
                {
                   numbers.Push(number);
                   
                   if (operations.TryPeek(out var peek) && peek != '(')
                   {
                       CalculateTheLastTwoNumbersOnTheStack();
                   }
                }
                else if (ch == ')')
                {
                    // Calculate an expression in the parenthesis
                    while (operations.Peek() != '(')
                    {
                        CalculateTheLastTwoNumbersOnTheStack();
                    }

                    operations.Pop(); // Remove '(' from the stack

                    // 
                    if (operations.Count != 0)
                    {
                        while (operations.Count != 0 && operations.Peek() != '(')
                        {
                            CalculateTheLastTwoNumbersOnTheStack();
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException($"Unexpected character: {ch}");
                }
                
                i++;
            }

            return numbers.Peek();

            void CalculateTheLastTwoNumbersOnTheStack()
            {
                long num1 = numbers.Pop();
                long num2 = numbers.Pop();
                var op = operations.Pop();
                var res = Calculate(num1, num2, op);
                numbers.Push(res);
            }
        }
    }
}