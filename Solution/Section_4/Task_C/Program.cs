using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Префиксные хеши (Получение хэша любой подстроки за О(1))
namespace Task_C;
/*
Алла не остановилась на достигнутом –— теперь она хочет научиться быстро вычислять хеши 
произвольных подстрок данной строки. Помогите ей!
На вход поступают запросы на подсчёт хешей разных подстрок.
Ответ на каждый запрос должен выполняться за O(1).
Допустимо в начале работы программы сделать предподсчёт для дальнейшей работы со строкой.
Напомним, что полиномиальный хеш считается по формуле.
В данной задаче необходимо использовать в качестве значений отдельных символов их коды в таблице ASCII.

Формат ввода
В первой строке дано число a (1 ≤ a ≤ 1000) –— основание, по которому считается хеш.
Во второй строке дано число m (1 ≤ m ≤ 107) –— модуль. 
В третьей строке дана строка s (0 ≤ |s| ≤ 106), состоящая из больших и маленьких латинских букв.
В четвертой строке дано число запросов t –— натуральное число от 1 до 105.
В каждой из следующих t строк записаны через пробел два числа l и r –
индексы начала и конца очередной подстроки. (1 ≤ l ≤ r ≤ |s|).

Формат вывода
Для каждого запроса выведите на отдельной строке хеш заданной в запросе подстроки.
 */
// https://algorithmica.org/ru/hashing

// Строка "abcdefgh"
// h(5,8) = h("efgh") = e*A^3 + f*A^2 + g*A^1 + h*A^0
// h(1,8) = h("abcdefgh") = a*A^7 + b*A^6 + c*A^5 + d*A^4 + e*A^3 + f*A^2 + g*A^1 + h*A^0
// h(1,8) = h("abcdefgh") = a*A^7 + b*A^6 + c*A^5 + d*A^4 + h(5,8)
// h(1,4) = h("efgh") = a*A^3 + b*A^2 + c*A^1 + d*A^0
// h(1,8) = h("abcdefgh") = h(1,4)*A^4 + h(5,8)  =>
// h(5,8) = h(1,8) - h (1,4) * A^4
internal class Program
{
    private static StreamReader _reader = new(Console.OpenStandardInput());
    private static long A = 0;
    private static long Mod = 0;
    private static string Str = string.Empty;
    private static readonly List<long> PowerA = new();
    private static readonly Dictionary<int, long> PrefixHashDict = new();

    static void Main(string[] args)
    {
        A = long.Parse(_reader.ReadLine());
        Mod = long.Parse(_reader.ReadLine());
        Str = _reader.ReadLine() ?? string.Empty;

        FillPrefixHashDictionary();
        FillPowers();
        Calculate();
    }

    /// <summary>
    /// Считаем h(1,1), h(1,2)....h(1,n)
    /// </summary>
    private static void FillPrefixHashDictionary()
    {
        // предподсчет
        int r = 1;
        PrefixHashDict.Add(1, Str[0] % Mod);

        for (int i = 1; i < Str.Length; i++)
        {
            var prefixHash = (PrefixHashDict[r] * A + Str[i]) % Mod;
            PrefixHashDict.Add(++r, prefixHash);
        }
    }

    private static void FillPowers()
    {
        PowerA.Add(1);

        for (int i = 1; i < Str.Length; i++)
        {
            long next = PowerA[i - 1] * A;
            next %= Mod;
            PowerA.Add(next);
        }
    }

    private static void Calculate()
    {
        long t = long.Parse(_reader.ReadLine());

        while (t != 0)
        {
            t--;
            var s = ReadList();
            var l = s[0];
            var r = s[1];
            long hash = PrefixHash(l, r);
            Console.WriteLine(hash);
        }
    }

    private static long PrefixHash(int l, int r)
    {
        if (l == 1)
        {
            return PrefixHashDict[r];
        }

        var hash = PrefixHashDict[r] - PrefixHashDict[l - 1] * PowerA[r - l + 1];

        if (hash < 0)
        {
            hash %= Mod;
            hash += Mod;
            hash %= Mod;
        }

        return hash;
    }

    private static List<int> ReadList() =>
    _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
}