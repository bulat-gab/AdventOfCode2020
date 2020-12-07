using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    public class Day3
    {
        public int Part1()
        {
            var matrix = Parse("./input3");

            return FindNumberOfTreesForSlope(matrix, 3, 1);
        }

        private static char[][] Parse(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var matrix = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                matrix[i] = lines[i].ToCharArray();
            }

            return matrix;
        }

        private int FindNumberOfTreesForSlope(char[][] matrix, int right, int down)
        {
            var rows = matrix.Length;
            var cols = matrix[0].Length;
            
            int treesNumber = 0;
            int currentRow = down;
            int currentCol = right;
            while (currentRow < rows)
            {
                if (currentCol >= cols)
                {
                    currentCol %= cols;
                }

                if (matrix[currentRow][currentCol] == '#')
                    treesNumber++;

                currentRow += down;
                currentCol += right;
            }

            return treesNumber;
        }

        public int Part2()
        {
            var matrix = Parse("./input3");

            var slope1 = FindNumberOfTreesForSlope(matrix, 1, 1);
            var slope2 = FindNumberOfTreesForSlope(matrix, 3, 1);
            var slope3 = FindNumberOfTreesForSlope(matrix, 5, 1);
            var slope4 = FindNumberOfTreesForSlope(matrix, 7, 1);
            var slope5 = FindNumberOfTreesForSlope(matrix, 1, 2);

            return slope1 * slope2 * slope3 * slope4 * slope5;
        }
    }
}