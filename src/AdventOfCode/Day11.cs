using System;
using System.IO;

namespace AdventOfCode
{
    public class Day11
    {
        private (int x, int y)[] _directions = {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1), 
            (1, -1), (1, 0), (1, 1),
        };
        
        public int Part1()
        {
            var matrix = BuildMatrix();
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            var copyOfMatrix = new char [rows, cols];
            
            var changed = true;
            while (changed)
            {
                changed = false;
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (matrix[i, j] == '.')
                        {
                            copyOfMatrix[i, j] = '.';
                        }
                        else if (matrix[i, j] == 'L')
                        {
                            if (TryToOccupy(matrix, copyOfMatrix, i, j)) 
                                changed = true; 
                        }
                        else if (matrix[i, j] == '#')
                        {
                            if (TryToEmpty(matrix, copyOfMatrix, i, j))
                                changed = true;
                        }
                    }
                }
                
                matrix = DeepCopy(copyOfMatrix);
            }
            return CountOccupiedPlaces(matrix);
        }
        
        public int Part2()
        {
            var matrix = BuildMatrix();
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            var copyOfMatrix = new char [rows, cols];
            
            var changed = true;
            while (changed)
            {
                changed = false;
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < cols; j++)
                    {
                        if (matrix[i, j] == '.')
                        {
                            copyOfMatrix[i, j] = '.';
                        }
                        else if (matrix[i, j] == 'L')
                        {
                            if (TryToOccupyPart2(matrix, copyOfMatrix, i, j)) 
                                changed = true; 
                        }
                        else if (matrix[i, j] == '#')
                        {
                            if (TryToEmptyPart2(matrix, copyOfMatrix, i, j))
                                changed = true;
                        }
                    }
                }
                
                matrix = DeepCopy(copyOfMatrix);
            }
            return CountOccupiedPlaces(matrix);
        }

        private bool TryToEmptyPart2(char[,] original, char[,] toModify, int x, int y)
        {
            var rows = original.GetLength(0);
            var cols = original.GetLength(1);

            var occupiedPlaces = 0;
            foreach (var (rowDir, colDir) in _directions)
            {
                var dx = rowDir;
                var dy = colDir;

                while (true)
                {
                    var nextX = x + dx;
                    var nextY = y + dy;
                
                    if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
                        break;

                    if (original[nextX, nextY] == '#')
                    {
                        occupiedPlaces++;
                        break;
                    }

                    if (original[nextX, nextY] == 'L')
                    {
                        break;
                    }

                    dx += dx;
                    dy += dy;
                }
                
            }

            if (occupiedPlaces < 5) 
                return false;
            
            toModify[x, y] = 'L';
            return true;
        }

        private bool TryToOccupyPart2(char[,] original, char[,] toModify, int x, int y)
        {
            var rows = original.GetLength(0);
            var cols = original.GetLength(1);

            foreach (var (rowDir, colDir) in _directions)
            {
                var dx = rowDir;
                var dy = colDir;

                // check one direction to the end
                while (true)
                {
                    var nextX = x + dx;
                    var nextY = y + dy;
                
                    if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
                        break;

                    if (original[nextX, nextY] == '#')
                        return false;
                    if (original[nextX, nextY] == 'L')
                        break;

                    dx += dx;
                    dy += dy;
                }
            }

            toModify[x, y] = '#';
            return true;
        }

        private char[,] BuildMatrix()
        {
            var lines = File.ReadAllLines("./input11");
            var matrix = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                for (int j = 0; j < lines[0].Length; j++)
                {
                    matrix[i, j] = line[j];
                }
            }
            
            return matrix;
        }

        private int CountOccupiedPlaces(char[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var occupied = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == '#')
                        occupied++;
                }
            }
            return occupied;
        }

        private char[,] DeepCopy(char[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            var copy = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copy[i, j] = matrix[i, j];
                }
            }

            return copy;
        }

        private bool TryToOccupy(char[,] original, char[,] toModify, int x, int y)
        {
            var rows = original.GetLength(0);
            var cols = original.GetLength(1);

            foreach (var (dx, dy) in _directions)
            {
                var nextX = x + dx;
                var nextY = y + dy;
                
                if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
                    continue;

                if (original[nextX, nextY] == '#')
                    return false;
            }

            toModify[x, y] = '#';
            return true;
        }
        
        private bool TryToEmpty(char[,] original, char[,] toModify, int x, int y)
        {
            var rows = original.GetLength(0);
            var cols = original.GetLength(1);

            var occupiedPlaces = 0;
            foreach (var (dx, dy) in _directions)
            {
                var nextX = x + dx;
                var nextY = y + dy;
                
                if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
                    continue;

                if (original[nextX, nextY] == '#')
                    occupiedPlaces++;
            }

            if (occupiedPlaces < 4) 
                return false;
            
            toModify[x, y] = 'L';
            return true;
        }
    }
}