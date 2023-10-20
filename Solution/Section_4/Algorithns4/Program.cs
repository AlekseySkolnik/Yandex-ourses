using System.Runtime.InteropServices;

namespace Algorithns4;

// https://algorithmica.org/ru/hashing
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static ulong Hash(ulong a, ulong mod, string str)
    {
        if (str.Length == 0)
        {
            return 0;
        }

        var hash = (byte)str[0] % mod;

        if (str.Length >= 2)
        {
            for (var i = 1; i < str.Length; i++)
            {
                hash = (hash * a + str[i]) % mod;
            }
        }

        return hash;
    }

    /// <summary>
    /// Такое решение имеет временную сложность O(n^2), так как у нас есть два вложенных цикла, а все манипуляции с хеш-таблицами занимают константное время.
    /// Предварительная сортировка элементов занимает O(nlogN), и мы можем не учитывать этот компонент, так как n^2 асимптотически больше nlogN
    /// </summary>
    public static HashSet<Tuple<int, int, int>> ThreeSum(int A, List<int> x)
    {
        var history = new HashSet<int>();
        int n = x.Count;

        var xSorted = new List<int>(x);
        xSorted.Sort();

        var triples = new HashSet<Tuple<int, int, int>>();

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                int target = A - xSorted[i] - xSorted[j];

                if (history.Contains(target))
                {
                    // Заметим, что тут тройка уже отсортирована за счёт предварительной сортировки всего массива.
                    triples.Add(Tuple.Create(target, xSorted[i], xSorted[j]));
                }

                history.Add(xSorted[i]);
            }
        }

        return triples;
    }
}
