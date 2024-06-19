using System;
using System.Collections.Generic;
using System.Linq;
using лаба10;
using ClassLibrary1;
namespace LabWork
{
    class Part2
    {
        static void Main(string[] args)
        {
            // Создание и заполнение коллекций из лабораторной работы 12
            var testCollections = new TestCollections();

            // Запросы с использованием LINQ

            // a) Where
            var pianos = from piano in testCollections.pianos.Values
                         where piano.NumberOfKeys > 50
                         select piano;

            // b) Count
            var pianoCount = (from piano in testCollections.pianos.Values
                              select piano).Count();

            // c) Aggregation (Sum, Max, Min, Average)
            var maxKeys = (from piano in testCollections.pianos.Values
                           select piano.NumberOfKeys).Max();

            // d) Group by
            var groupedByLayout = from piano in testCollections.pianos.Values
                                  group piano by piano.KeyboardLayout;

            // Запросы с использованием методов расширения

            // a) Where
            var pianosExt = testCollections.pianos.Values.Where(piano => piano.NumberOfKeys > 50);

            // b) Count
            var pianoCountExt = testCollections.pianos.Values.Count();

            // c) Aggregation (Sum, Max, Min, Average)
            var maxKeysExt = testCollections.pianos.Values.Max(piano => piano.NumberOfKeys);

            // d) Group by
            var groupedByLayoutExt = testCollections.pianos.Values.GroupBy(piano => piano.KeyboardLayout);

            // Вывод результатов запросов
            Console.WriteLine("Запросы с использованием LINQ:");
            Console.WriteLine("Where:");
            foreach (var piano in pianos)
                Console.WriteLine(piano.Name);

            Console.WriteLine("Count:");
            Console.WriteLine(pianoCount);

            Console.WriteLine("Max:");
            Console.WriteLine(maxKeys);

            Console.WriteLine("Group by:");
            foreach (var group in groupedByLayout)
            {
                Console.WriteLine(group.Key);
                foreach (var piano in group)
                {
                    Console.WriteLine($"  {piano.Name}");
                }
            }

            // Вывод результатов запросов с методами расширения
            Console.WriteLine("Запросы с использованием методов расширения:");
            Console.WriteLine("Where:");
            foreach (var piano in pianosExt)
                Console.WriteLine(piano.Name);

            Console.WriteLine("Count:");
            Console.WriteLine(pianoCountExt);

            Console.WriteLine("Max:");
            Console.WriteLine(maxKeysExt);

            Console.WriteLine("Group by:");
            foreach (var group in groupedByLayoutExt)
            {
                Console.WriteLine(group.Key);
                foreach (var piano in group)
                {
                    Console.WriteLine($"  {piano.Name}");
                }
            }
        }
    }
}
