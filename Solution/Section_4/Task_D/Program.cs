using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Кружки
namespace Task_D;
/*
В компании, где работает Тимофей, заботятся о досуге сотрудников и устраивают различные кружки по интересам. 
Когда кто-то записывается на занятие, в лог вносится название кружка.
По записям в логе составьте список всех кружков, в которые ходит хотя бы один человек.

Формат ввода
В первой строке даётся натуральное число n, не превосходящее 10 000 –— количество записей в логе.
В следующих n строках —– названия кружков.

Формат вывода
Выведите уникальные названия кружков по одному на строке, в порядке появления во входных данных.
 */

internal class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var count = int.Parse(_reader.ReadLine());
        SecondVariant(count);
    }

    private static void FirstVariant(int count)
    {
        var repository = new HashSet<string>();

        for (int i = 0; i < count; i++)
        {
            var str = _reader.ReadLine();

            if (repository.Contains(str))
            {
                continue;
            }

            repository.Add(str);
        }

        repository.ToList().ForEach(x => Console.WriteLine(x));
    }

    private static void SecondVariant(int count)
    {
        var repository = new Hashtable();
        var list = new List<string>();

        for (int i = 0; i < count; i++)
        {
            var str = _reader.ReadLine() ?? string.Empty;

            if (repository.ContainsKey(str))
            {
                continue;
            }

            repository.Add(str, i);
            list.Add(str);
        }

        foreach (var str in list)
        {
            Console.WriteLine(str);
        }
    }

    private static void ThirdVariant(int count)
    {
        var repository = new Dictionary<string, int>();
        var index = 0;

        for (int i = 0; i < count; i++)
        {
            var str = _reader.ReadLine() ?? string.Empty;

            if (repository.ContainsKey(str))
            {
                continue;
            }

            repository.Add(str, ++index);
        }

        foreach (var str in repository.OrderBy(x => x.Value))
        {
            Console.WriteLine(str.Key);
        }
    }
}
