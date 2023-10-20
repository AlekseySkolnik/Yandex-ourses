using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Полиномиальный хеш
namespace Task_A;
/*
Алле очень понравился алгоритм вычисления полиномиального хеша.
Помогите ей написать функцию, вычисляющую хеш строки s. 
В данной задаче необходимо использовать в качестве значений отдельных символов их коды в таблице ASCII.
Полиномиальный хеш считается по формуле:
 */

internal class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var a = ulong.Parse(_reader.ReadLine());
        var mod = ulong.Parse(_reader.ReadLine());
        var str = _reader.ReadLine()?.Select(x => (byte)x).ToList() ?? new List<byte>();

        SecondVariant(a, mod, str);
    }

    // h("GOSHA")=(h("GOSH")⋅251+ A~)mod 256 = (181⋅251 + 65) mod 256 = 184
    private static void SecondVariant(ulong a, ulong mod, List<byte> str)
    {
        if (str.Count == 0)
        {
            Console.WriteLine(0);
            return;
        }

        var hash = str[0] % mod;

        if (str.Count >= 2)
        {
            for (var i = 1; i < str.Count; i++)
            {
                hash = (hash * a + str[i]) % mod;
            }
        }

        Console.WriteLine(hash);
    }

    // не прокатит, переполнение
    private static void FirstVariant(ulong a, ulong m, List<byte> str)
    {
        var n = str.Count;
        var powers = new ulong[n];
        powers[n - 1] = 1;

        for (int i = n - 2; i >= 0; i--)
        {
            powers[i] = powers[i + 1] * a;
        }

        ulong sum = 0;

        for (int i = 0; i < n; i++)
        {
            sum += str[i] * powers[i];
        }

        Console.WriteLine(sum % m);
    }
}
