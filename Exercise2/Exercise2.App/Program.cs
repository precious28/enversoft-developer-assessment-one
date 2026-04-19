using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exercise2.App
{
    public class CsvProcessor
    {
        public record Person(string FirstName, string LastName, string Address);

        public static List<Person> ReadCsv(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l =>
                {
                    var parts = l.Split(',');
                    return new Person(parts[0].Trim(), parts[1].Trim(), parts[2].Trim());
                })
                .ToList();
        }

        public static IEnumerable<string> GetNameFrequencies(List<Person> people)
        {
            return people.SelectMany(p => new[] { p.FirstName, p.LastName })
                .GroupBy(n => n)
                .Select(g => (Name: g.Key, Count: g.Count()))
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Name)
                .Select(x => $"{x.Name},{x.Count}");
        }

        public static IEnumerable<string> GetAddressesSortedByStreetName(List<Person> people)
        {
            return people.Select(p => p.Address)
                .OrderBy(a =>
                {
                    // Extract the street name (skip leading number if present)
                    var parts = a.Split(' ');
                    return int.TryParse(parts[0], out _)
                        ? string.Join(" ", parts.Skip(1))
                        : a;
                });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string csvPath = args.Length > 0 ? args[0] : "Data.csv";
            var people = CsvProcessor.ReadCsv(csvPath);

            File.WriteAllLines("name_frequencies.txt", CsvProcessor.GetNameFrequencies(people));
            File.WriteAllLines("addresses_sorted.txt", CsvProcessor.GetAddressesSortedByStreetName(people));

            Console.WriteLine("Done. Output written to name_frequencies.txt and addresses_sorted.txt");
        }
    }
}
