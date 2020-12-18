using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AdventOfCode
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void Run()
        {
            var d = new Day18().Part1();
            Console.WriteLine(d); // 17827727081 / 647857897
        }
        
        [Test]
        public void ParsingTest1()
        {
            var line = "light red bags contain 1 bright white bag, 2 muted yellow bags.";
            
            var split = line.Split("contain");
            var indexOfBags = split[0].IndexOf("bags");
            var parentNode = split[0].Substring(0, indexOfBags - 1);


            if (line.Contains("no other bags"))
            {
                //continue;
            }
            
            var childrenData = split[1].Split(',');
            var regex = new Regex("^ [0-9]+ ([a-z]+ [a-z]+) bags*");
            foreach (var data in childrenData)
            {
                var match = regex.Match(data);
                var childNode = match.Groups[1].Value;
            }
            
            Assert.AreEqual(parentNode, "light red");
        }
        
        [Test]
        public void Day7Test()
        {
            var part1 = new Day7().Part1();
            var part2 = new Day7().Part2();
            
            Assert.AreEqual(89084, part2);
            Assert.AreEqual(185, part1);
        }

        [TestCase("1 + (2 * 3)", 7)]
        [TestCase("2 * 3 + (4 * 5)", 26)]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [TestCase("1 + 2 * 3 + 4 * 5 + 6", 71)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void Day18Test(string input, int expected)
        {
            var d18 = new Day18();
            
            var actual = d18.Evaluate(input);
            
            Assert.AreEqual(expected, actual);
        }
    }
}