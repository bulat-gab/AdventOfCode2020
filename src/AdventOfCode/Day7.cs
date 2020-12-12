using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day7
    {
        private const string MyBag = "shiny gold";
        
        public int Part1()
        {
            var lines = Parse();
            var graph = BuildGraph(lines);

            var outerBagsCount = 0;
            var visited = new HashSet<string>();
            var stack = new Stack<string>();

            foreach (var source in FindSourcesThatSinkInto(MyBag))
            {
                stack.Push(source);
            }

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                if (visited.Contains(current))
                    continue;
                
                visited.Add(current);
                outerBagsCount++;
                
                // Find all source vertices, whose edges sink into 'current' vertex
                foreach (var source in FindSourcesThatSinkInto(current))
                {
                    if (!visited.Contains(source))
                        stack.Push(source);
                }
            }

            return outerBagsCount;
            
            IEnumerable<string> FindSourcesThatSinkInto(string target)
            {
                foreach (var (vertex, row) in graph)
                {
                    if (row.TryGetValue(target, out _))
                    {
                        yield return vertex;
                    }
                }
            }
        }
        
        public int Part2()
        {
            var lines = Parse();
            var graph = BuildGraph(lines);
            return RecursiveDfsPart2(graph, MyBag);
        }

        private int RecursiveDfsPart2(Dictionary<string, Dictionary<string, int>> graph, string vertex)
        {
            var sinks = FindSinksOf(vertex);

            var innerBagsCount = 0;
            foreach (var (sinkName, numberOfBags) in sinks)
            {
                innerBagsCount += numberOfBags + (numberOfBags * RecursiveDfsPart2(graph, sinkName));
            }

            return innerBagsCount;
            
            IEnumerable<(string vertex, int numberOfBags)> FindSinksOf(string target)
            {
                var isContains = graph.TryGetValue(target, out var row);
                if (!isContains)
                    yield break;
                
                foreach (var (sink, numberOfBags) in row)
                {
                    yield return (sink, numberOfBags);
                }
            }
        }

        private Dictionary<string, Dictionary<string, int>>BuildGraph(string[] lines)
        {
            var graph = new Dictionary<string, Dictionary<string, int>>();
            
            foreach (var line in lines)
            {
                if (line.Contains("no other bags"))
                    continue;

                var parent = GetParentNode(line);
                var children = GetChildrenNodes(line);

                graph[parent] = new Dictionary<string, int>();
                
                foreach (var (bagsNumber, child) in children)
                {
                    graph[parent][child] = bagsNumber;
                }
            }

            return graph;
            
            string GetParentNode(string line)
            {
                var split = line.Split("contain");
                var indexOfBags = split[0].IndexOf("bags");
                var parentNode = split[0].Substring(0, indexOfBags - 1);
                return parentNode;
            }
            
            IEnumerable<(int, string)> GetChildrenNodes(string line)
            {
                var regex = new Regex("^ ([0-9]+) ([a-z]+ [a-z]+) bags*");
            
                var split = line.Split("contain");
                var childrenData = split[1].Split(',');
                foreach (var data in childrenData)
                {
                    var match = regex.Match(data);
                    var childNode = match.Groups[2].Value;
                    var bagsNumber = int.Parse(match.Groups[1].Value);
                    yield return (bagsNumber, childNode);
                }
            }
        }

        private static string[] Parse() => File.ReadAllLines("./input7");
    }
}