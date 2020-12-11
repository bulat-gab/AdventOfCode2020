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
            var d = new Day11().Part2();
            Console.WriteLine(d);
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
    }
}