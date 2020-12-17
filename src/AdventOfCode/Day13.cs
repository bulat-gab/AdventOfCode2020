using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day13
    {
        private string[] lines = File.ReadAllLines("./input13");

        public int Part1()
        {
            var (departureTime, busIds) = ParseInput();

            var timestamps = new int[busIds.Length];
            for (var i = 0; i < busIds.Length; i++)
            {
                timestamps[i] = busIds[i];
                while (timestamps[i] < departureTime)
                {
                    timestamps[i] += busIds[i];
                }
            }

            var min = int.MaxValue;
            var minIndex = -1;
            for (int i = 0; i < timestamps.Length; i++)
            {
                if (timestamps[i] < min)
                {
                    min = timestamps[i];
                    minIndex = i;
                }
            }

            var waitingTime = min - departureTime;
            
            return busIds[minIndex] * waitingTime;
        }

        public long Part2()
        {
            long target = -1;
            var busIds = lines[1].Split(',');
            var busIdsLengthWithoutX = busIds.Count(x => x != "x");
            var timestamps = new (int id, int offset, long currentTimestamp)[busIdsLengthWithoutX];

            var timestampIndex = 0;
            for (var i = 0; i < busIds.Length; i++)
            {
                if (int.TryParse(busIds[i], out var parsedId))
                {
                    timestamps[timestampIndex++] = (parsedId, i, parsedId);
                    if (target == -1)
                    {
                        target = parsedId;
                    }
                }
            }
            
            var earliestTimestamp = target;
            while (true)
            {
                var goodBuses = 0;
                for (var i = 0; i < timestamps.Length; i++)
                {
                    var (id, offset, _) = timestamps[i];
                    
                    while (timestamps[i].currentTimestamp + offset < earliestTimestamp)
                    {
                        timestamps[i] = (id, offset, timestamps[i].currentTimestamp + id);
                    }

                    if (timestamps[i].currentTimestamp + offset > earliestTimestamp)
                    {
                        break;
                    }
                    else if (timestamps[i].currentTimestamp + offset == earliestTimestamp)
                    {
                        goodBuses++;
                    }
                }

                if (goodBuses == timestamps.Length)
                {
                    return earliestTimestamp;
                }

                earliestTimestamp += target;
                // Console.WriteLine(earliestTimestamp);
            }


            return 0;
        }

        private (int departureTime, int[] busIds) ParseInput()
        {
            var departureTime = int.Parse(lines[0]);
            var busIds = lines[1]
                .Split(',')
                .Where(x => x != "x")
                .Select(int.Parse)
                .ToArray();
            return (departureTime, busIds);
        }
    }
}