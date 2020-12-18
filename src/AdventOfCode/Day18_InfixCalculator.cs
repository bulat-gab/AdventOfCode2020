using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public partial class Day18
    {
        private string[] lines = File.ReadAllLines("./input18");

        public long Part1()
        {
            long sum = 0;
            foreach (var line in lines)
            {
                sum += Evaluate(line);
            }
            
            return sum;
        }

        public long Evaluate(string line)
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
                       CalculateTheLastTwoNumbersOnTheStack(numbers, operations);
                   }
                }
                else if (ch == ')')
                {
                    // Calculate an expression in the parenthesis
                    while (operations.Peek() != '(')
                    {
                        CalculateTheLastTwoNumbersOnTheStack(numbers, operations);
                    }

                    operations.Pop(); // Remove '(' from the stack

                    // 
                    if (operations.Count != 0)
                    {
                        while (operations.Count != 0 && operations.Peek() != '(')
                        {
                            CalculateTheLastTwoNumbersOnTheStack(numbers, operations);
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
        }

        private void CalculateTheLastTwoNumbersOnTheStack(Stack<long> numbers, Stack<char> operations)
        {
            long num1 = numbers.Pop();
            long num2 = numbers.Pop();
            var op = operations.Pop();
            
            var res = Calculate(num1, num2, op);
            numbers.Push(res);
        }
        
        private long Calculate(long num1, long num2, char op)
        {
            return op switch
            {
                '+' => num1 + num2,
                '*' => num1 * num2,
                _ => throw new NotImplementedException($"Operation {op} is not implemented")
            };
        }
    }
}