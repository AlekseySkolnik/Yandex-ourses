using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Сломай меня
namespace Task_B;
/*
Гоша написал программу, которая сравнивает строки исключительно по их хешам.
Если хеш равен, то и строки равны. 
Тимофей увидел это безобразие и поручил вам сломать программу Гоши, чтобы остальным неповадно было.
В этой задаче вам надо будет лишь найти две различные строки, 
которые для заданной хеш-функции будут давать одинаковое значение.
Гоша использует следующую хеш-функцию:
 */

internal class Program
{
    const ulong A = 1_000;
    const ulong Mod = 123_987_123;

    static void Main(string[] args)
    {
        var dict = new Dictionary<ulong, string>();

        while (true)
        {
            var word = RandomString(20);
            var hash_val = Hash(A, Mod, word);

            if (!dict.ContainsKey(hash_val))
            {
                dict[hash_val] = word;
            }
            else
            {
                Console.WriteLine($"hash({word}) = {hash_val}");
                Console.WriteLine($"dict[{hash_val}] = {dict[hash_val]}");
                break;
            }
        }
    }

    private static ulong Hash(ulong a, ulong mod, string str)
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

    private static string RandomString(int count = 20)
    {
        var rnd = new Random();
        var s = string.Empty;

        for (int i = 0; i < count; i++)
        {
            var code = rnd.Next(97, 122);
            s += char.ConvertFromUtf32(code);
        }

        return s;
    }
}
