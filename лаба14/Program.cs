using System;
using System.Collections.Generic;
using System.Linq;
using лаба10;
namespace LabWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // Коллекция 1: SortedDictionary
            var concertParticipants = new SortedDictionary<string, List<MusicalInstrument>>();

            // Заполнение коллекции объектами
            var instruments = new List<MusicalInstrument>
            {
                new Guitar(6, "Guitar1", 1),
                new ElectricGuitar("Battery", "ElectricGuitar1", 6, 2),
                new Piano(88, "Piano1", "Grand", 3)
            };

            concertParticipants.Add("Band1", instruments);
            concertParticipants.Add("Band2", new List<MusicalInstrument>
            {
                new Guitar(6, "Guitar2", 4),
                new ElectricGuitar("USB", "ElectricGuitar2", 7, 5),
                new Piano(88, "Piano2", "Upright", 6)
            });

            // Запросы с использованием LINQ

            // a) Where
            var guitars = from band in concertParticipants
                          from instrument in band.Value
                          where instrument is Guitar
                          select instrument;

            // b) Union, Except, Intersect
            var guitarsBand1 = concertParticipants["Band1"].Where(i => i is Guitar);
            var guitarsBand2 = concertParticipants["Band2"].Where(i => i is Guitar);

            var unionGuitars = guitarsBand1.Union(guitarsBand2);
            var exceptGuitars = guitarsBand1.Except(guitarsBand2);
            var intersectGuitars = guitarsBand1.Intersect(guitarsBand2);

            // c) Aggregation (Sum, Max, Min, Average)
            var maxStrings = concertParticipants.Values
                .SelectMany(instrumentsList => instrumentsList.OfType<Guitar>())
                .Max(g => g.NumberOfStrings);

            // d) Group by
            var groupedByType = from band in concertParticipants
                                from instrument in band.Value
                                group instrument by instrument.GetType();

            // e) Let
            var letQuery = from band in concertParticipants
                           from instrument in band.Value
                           let type = instrument.GetType().Name
                           select new { Type = type, Instrument = instrument.Name };

            // f) Join
            var joinedQuery = from band in concertParticipants
                              from instrument in band.Value
                              join guitar in concertParticipants.Values.SelectMany(i => i).OfType<Guitar>()
                              on instrument.Name equals guitar.Name
                              select new { Band = band.Key, Instrument = instrument.Name };

            // Запросы с использованием методов расширения

            // a) Where
            var guitarsExt = concertParticipants.SelectMany(band => band.Value)
                                                .Where(instrument => instrument is Guitar);

            // b) Union, Except, Intersect
            var unionGuitarsExt = guitarsBand1.Union(guitarsBand2);
            var exceptGuitarsExt = guitarsBand1.Except(guitarsBand2);
            var intersectGuitarsExt = guitarsBand1.Intersect(guitarsBand2);

            // c) Aggregation (Sum, Max, Min, Average)
            var maxStringsExt = concertParticipants.Values
                .SelectMany(instrumentsList => instrumentsList.OfType<Guitar>())
                .Max(g => g.NumberOfStrings);

            // d) Group by
            var groupedByTypeExt = concertParticipants.SelectMany(band => band.Value)
                                                      .GroupBy(instrument => instrument.GetType());

            // e) Let
            var letQueryExt = concertParticipants.SelectMany(band => band.Value)
                                                 .Select(instrument => new { Type = instrument.GetType().Name, Instrument = instrument.Name });

            // f) Join
            var joinedQueryExt = concertParticipants.SelectMany(band => band.Value)
                                                    .Join(concertParticipants.Values.SelectMany(i => i).OfType<Guitar>(),
                                                          instrument => instrument.Name,
                                                          guitar => guitar.Name,
                                                          (instrument, guitar) => new { Band = "Band", Instrument = instrument.Name });

            // Вывод результатов запросов
            Console.WriteLine("Запросы с использованием LINQ:");
            Console.WriteLine("Where:");
            foreach (var guitar in guitars)
                Console.WriteLine(guitar.Name);

            Console.WriteLine("Union:");
            foreach (var guitar in unionGuitars)
                Console.WriteLine(guitar.Name);

            Console.WriteLine("Max:");
            Console.WriteLine(maxStrings);

            Console.WriteLine("Group by:");
            foreach (var group in groupedByType)
            {
                Console.WriteLine(group.Key.Name);
                foreach (var instrument in group)
                {
                    Console.WriteLine($"  {instrument.Name}");
                }
            }

            Console.WriteLine("Let:");
            foreach (var item in letQuery)
            {
                Console.WriteLine($"Type: {item.Type}, Instrument: {item.Instrument}");
            }

            Console.WriteLine("Join:");
            foreach (var item in joinedQuery)
            {
                Console.WriteLine($"Band: {item.Band}, Instrument: {item.Instrument}");
            }

            // Вывод результатов запросов с методами расширения
            Console.WriteLine("Запросы с использованием методов расширения:");
            Console.WriteLine("Where:");
            foreach (var guitar in guitarsExt)
                Console.WriteLine(guitar.Name);

            Console.WriteLine("Union:");
            foreach (var guitar in unionGuitarsExt)
                Console.WriteLine(guitar.Name);

            Console.WriteLine("Max:");
            Console.WriteLine(maxStringsExt);

            Console.WriteLine("Group by:");
            foreach (var group in groupedByTypeExt)
            {
                Console.WriteLine(group.Key.Name);
                foreach (var instrument in group)
                {
                    Console.WriteLine($"  {instrument.Name}");
                }
            }

            Console.WriteLine("Let:");
            foreach (var item in letQueryExt)
            {
                Console.WriteLine($"Type: {item.Type}, Instrument: {item.Instrument}");
            }

            Console.WriteLine("Join:");
            foreach (var item in joinedQueryExt)
            {
                Console.WriteLine($"Band: {item.Band}, Instrument: {item.Instrument}");
            }
        }
    }
}
