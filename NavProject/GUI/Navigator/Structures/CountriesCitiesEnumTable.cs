using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Navigator.Structures
{
    static class CountriesCitiesEnumTable
    {
        static private string[] countries;
        static private Dictionary<string,List<string>> cities;

        static CountriesCitiesEnumTable() => ReadRegions();
        static private void ReadRegions()
        {
            // Temp
            countries = new string[] { "Россия", "USA" };
            cities = new Dictionary<string, List<string>>()
            {
                ["Россия"] = new List<string>() { "Краснодар","Ростов" },
                ["USA"] = new List<string>() { "Atlanta", "NYC" },
            };
            //ReadCitiesFromFile
        }
        static public string[] GetCountries() => countries;
        static public List<string> GetCities(string country) => cities[country];
    }
}
