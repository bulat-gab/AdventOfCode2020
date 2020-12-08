using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day4
    {
        private const string byr = "byr";
        private const string iyr = "iyr";
        private const string eyr = "eyr";
        private const string hgt = "hgt";
        private const string hcl = "hcl";
        private const string ecl = "ecl";
        private const string pid = "pid";
        private const string cid = "cid";

        private readonly IEnumerable<string> _eyeColors = new [] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        
        public int Part1()
        {
            var input = File.ReadAllText("./input4");
            var passports = input.Split("\r\n\r\n");

            var validPassorts = 0;
            foreach (var passport in passports)
            {
                // var entries = passport.Split(new[] {"\r\n", " "}, System.StringSplitOptions.None);

                if (passport.Contains(byr)
                    && passport.Contains(iyr)
                    && passport.Contains(eyr)
                    && passport.Contains(hgt)
                    && passport.Contains(hcl)
                    && passport.Contains(ecl)
                    && passport.Contains(pid))
                {
                    validPassorts++;
                }
                
            }
            
            return validPassorts;
        }

        public int Part2()
        {
            var input = File.ReadAllText("./input4");
            var passports = input.Split("\r\n\r\n");
            
            var validPassorts = 0;
            foreach (var passport in passports)
            {
                var entries = passport.Split(new[] {"\r\n", " "}, System.StringSplitOptions.None);
                var dict = entries.ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1]);

                string v;
                if (!CheckBirthDate(dict)) 
                    continue;

                if (dict.TryGetValue(iyr, out v))
                {
                    var parsed = int.TryParse(v, out var issueYear);
                    if (!parsed || issueYear < 2010 || issueYear > 2020)
                        continue;
                }
                else
                {
                    continue;
                }

                if (dict.TryGetValue(eyr, out v))
                {
                    var parsed = int.TryParse(v, out var expirationYear);
                    if (!parsed || expirationYear < 2020 || expirationYear > 2030)
                        continue;
                }
                else
                {
                    continue;
                }
                
                if (dict.TryGetValue(hgt, out v))
                {
                    var heightString = v.Substring(0, v.Length - 2);
                    var parsed = int.TryParse(heightString, out var parsedValue);
                    if (!parsed)
                        continue;
                    
                    if (v.Contains("cm"))
                    {
                        if (parsedValue < 150 || parsedValue > 193)
                            continue;
                    }

                    if (v.Contains("in"))
                    {
                        if (parsedValue < 59 || parsedValue > 76)
                            continue;
                    }
                }
                else
                {
                    continue;
                }
                
                if (dict.TryGetValue(hcl, out v))
                {
                    if (!Regex.IsMatch(v, "^#[a-zA-Z0-9]{6}$"))
                        continue;
                }
                else
                {
                    continue;
                }
                
                if (dict.TryGetValue(ecl, out v))
                {
                    if (!_eyeColors.Contains(v))
                        continue;
                }
                else
                {
                    continue;
                }
                
                if (dict.TryGetValue(pid, out v))
                {
                    if (!Regex.IsMatch(v, "^[0-9]{9}$"))
                        continue;
                }
                else
                {
                    continue;
                }
                
                validPassorts++;
            }
            
            return validPassorts;
        }

        private static bool CheckBirthDate(Dictionary<string, string> dict)
        {
            if (!dict.TryGetValue(byr, out var v))
            {
                return false;
            }

            var parsed = int.TryParse(v, out var birthYear);

            return parsed && birthYear >= 1920 && birthYear <= 2002;
        }
    }
}