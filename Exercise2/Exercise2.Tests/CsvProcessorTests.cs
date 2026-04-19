using System.Collections.Generic;
using System.Linq;
using Exercise2.App;
using Xunit;

namespace Exercise2.Tests
{
    public class CsvProcessorTests
    {
        private static List<CsvProcessor.Person> SamplePeople() => new()
        {
            new("Jimmy",  "Smith",  "102 Long Lane"),
            new("Clive",  "Owen",   "65 Ambling Way"),
            new("James",  "Brown",  "82 Stewart St"),
            new("Graham", "Howe",   "12 Howard St"),
            new("John",   "Howe",   "78 Short Lane"),
            new("Clive",  "Smith",  "49 Sutherland St"),
            new("James",  "Owen",   "8 Crimson Rd"),
            new("Graham", "Brown",  "94 Roland St"),
        };

        [Fact]
        public void NameFrequencies_OrderedByCountDescThenAlpha()
        {
            var result = CsvProcessor.GetNameFrequencies(SamplePeople()).ToList();

            // All names with count 2 come before count 1
            Assert.Equal("Brown,2", result[0]);
            Assert.Equal("Clive,2", result[1]);
            Assert.Equal("Graham,2", result[2]);
            Assert.Equal("Howe,2", result[3]);
            Assert.Equal("James,2", result[4]);
            Assert.Equal("Owen,2", result[5]);
            Assert.Equal("Smith,2", result[6]);
            Assert.Equal("Jimmy,1", result[7]);
            Assert.Equal("John,1", result[8]);
        }

        [Fact]
        public void Addresses_SortedByStreetName()
        {
            var result = CsvProcessor.GetAddressesSortedByStreetName(SamplePeople()).ToList();

            Assert.Equal("65 Ambling Way",   result[0]);
            Assert.Equal("8 Crimson Rd",     result[1]);
            Assert.Equal("12 Howard St",     result[2]);
            Assert.Equal("102 Long Lane",    result[3]);
            Assert.Equal("94 Roland St",     result[4]);
            Assert.Equal("78 Short Lane",    result[5]);
            Assert.Equal("82 Stewart St",    result[6]);
            Assert.Equal("49 Sutherland St", result[7]);
        }

        [Fact]
        public void NameFrequencies_EmptyList_ReturnsEmpty()
        {
            var result = CsvProcessor.GetNameFrequencies(new List<CsvProcessor.Person>());
            Assert.Empty(result);
        }
    }
}
