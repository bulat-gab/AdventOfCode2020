// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Text.RegularExpressions;
//
// namespace AdventOfCode
// {
//     public class Day7
//     {
//         public int Part1()
//         {
//             var lines = Parse();
//             var graph = BuildGraph(lines);
//
//
//             var visited = new HashSet<string>();
//             var stack
//             
//             
//             return -1;
//         }
//
//         private Dictionary<string, Dictionary<string, int>>BuildGraph(string[] lines)
//         {
//             var adjacencyList = new Dictionary<string, List<string>>();
//             var regex = new Regex("^ [0-9]+ ([a-z]+ [a-z]+) bags*");
//             
//             foreach (var line in lines)
//             {
//                 if (line.Contains("no other bags"))
//                     continue;
//
//                 var split = line.Split("contain");
//                 var indexOfBags = split[0].IndexOf("bags");
//                 var parentNode = split[0].Substring(0, indexOfBags - 1);
//
//                 var children = new List<string>();
//                 var childrenData = split[1].Split(',');
//                 foreach (var data in childrenData)
//                 {
//                     var match = regex.Match(data);
//                     var childNode = match.Groups[1].Value;
//                     children.Add(childNode);
//                 }
//                 
//                 adjacencyList.Add(parentNode, children);
//             }
//
//             return adjacencyList;
//         }
//         
//         private static string[] Parse() => File.ReadAllLines("./input7");
//     }
// }