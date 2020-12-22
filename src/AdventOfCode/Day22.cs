using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day22
    {
        private string[] _lines;

        public Day22(string inputPath = "./input22")
        {
            _lines = File.ReadAllLines(inputPath);
        }
        
        public int Part1()
        {
            var (player1, player2) = ParseInput();

            var numberOfCardsToWin = player1.Count * 2;
            
            while (true)
            {
                if (player1.Count == numberOfCardsToWin)
                {
                    return CalculateWinner(player1, numberOfCardsToWin);
                }

                if (player2.Count == numberOfCardsToWin)
                {
                    return CalculateWinner(player2, numberOfCardsToWin);
                }
                
                var cardOfPlayer1 = player1[0];
                player1.RemoveAt(0);
                var cardOfPlayer2 = player2[0];
                player2.RemoveAt(0);

                if (cardOfPlayer1 > cardOfPlayer2)
                {
                    player1.Add(cardOfPlayer1);
                    player1.Add(cardOfPlayer2);
                }
                else if (cardOfPlayer1 < cardOfPlayer2)
                {
                    player2.Add(cardOfPlayer2);
                    player2.Add(cardOfPlayer1);
                }
                else
                {
                    throw new NotImplementedException("Card values can not be equal");
                }

            }
            
        }

        private int CalculateWinner(List<int> cards, int numberOfCardsToWin)
        {
            int score = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                score = score + (cards[i] * numberOfCardsToWin);
                numberOfCardsToWin--;
            }
            
            Console.WriteLine($"Post-game deck: {string.Join(", ",cards)}");
            return score;
        }

        /// <summary>
        /// Player1 List contains: 9 2 6 3 1 
        /// </summary>
        /// <returns></returns>
        private (List<int>, List<int>) ParseInput()
        {
            var player1 = new List<int>();
            var player2 = new List<int>();
            
            var forPlayer1 = true;
            foreach (var line in _lines)
            {
                if (line.Contains("Player 2"))
                {
                    forPlayer1 = false;
                    continue;
                }

                if (int.TryParse(line, out var cardValue))
                {
                    if (forPlayer1)
                    {
                        player1.Add(cardValue);
                    }
                    else
                    {
                        player2.Add(cardValue);
                    }
                }
            }

            Console.WriteLine($"P1: {string.Join(", ",player1)}");
            Console.WriteLine($"P2: {string.Join(", ",player2)}");
            
            return (player1, player2);
        }

        public int Part2()
        {

            return 0;
        }

        
    }
}