using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Сумма четвёрок
namespace Task_J;
/*
У Гоши есть любимое число S. 
Помогите ему найти все уникальные четвёрки чисел в массиве, которые в сумме дают заданное число S.

Формат ввода
В первой строке дано общее количество элементов массива n (0 ≤ n ≤ 1000).
Во второй строке дано целое число S.
В третьей строке задан сам массив. 
Каждое число является целым и не превосходит по модулю 109.

Формат вывода
В первой строке выведите количество найденных четвёрок чисел.
В последующих строках выведите найденные четвёрки. 
Числа внутри одной четверки должны быть упорядочены по возрастанию.
Между собой четвёрки упорядочены лексикографически.
 */

internal class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {

        var docDict = new Dictionary<int, int>()
        {
            { 1, 2 }, { 2, 4}, { 3,2}
        };

        var mas = docDict.ToArray();
        Array.Sort(mas, (a, b) =>
        {
            if (a.Value > b.Value)
            {
                return -1;
            }

            if (a.Value < b.Value)
            {
                return 1;
            }

            return a.Key - b.Key;
        });


        var n = short.Parse(_reader.ReadLine());
        var s = ulong.Parse(_reader.ReadLine());
        // var mas = _reader.ReadLine()?.Select(x => (ulong)x).ToArray();
    }

    private static HashSet<Tuple<int, int, int>> ThreeSum(int A, List<int> mas)
    {
        var history = new HashSet<int>();
        var masSorted = new List<int>(mas);
        masSorted.Sort();

        var triples = new HashSet<Tuple<int, int, int>>();

        for (int i = 0; i < masSorted.Count; i++)
        {
            for (int j = i + 1; j < masSorted.Count; j++)
            {
                int target = A - masSorted[i] - masSorted[j];

                if (history.Contains(target))
                {
                    // Заметим, что тут тройка уже отсортирована за счёт предварительной сортировки всего массива.
                    triples.Add(Tuple.Create(target, masSorted[i], masSorted[j]));
                }

                history.Add(masSorted[i]);
            }
        }

        return triples;
    }
}
