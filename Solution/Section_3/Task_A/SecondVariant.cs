using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_A;
public static class SecondVariant
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    public static void SolvedParentheses()
    {
        var n = byte.Parse(_reader.ReadLine());

        foreach (var s in GenerateParentheses("(", 2 * n, 1, 0))
        {
            Console.WriteLine(s);
        }
    }

    /*
     * Можно заметить, что для любых правильных A и B такая C тоже будет правильной: C = (A)B
     * Если C длины 2n, то надо брать все A длины 2k и все B длины 2(n - k - 1) для всех валидных k.
     * За тем, чтобы понять, что это корректно, стоит простая идея.
     * Возьмём любую ПСП, и у первой её скобки найдём парную. 
     * Тогда внутри этих скобок как раз останется A, а за ними B.
     * Этот алгоритм уникально перебирает все ПСП, но порядок у них будет не лексикографический. 
     * Есть алгоритм, лишённый этого недостатка, но он не рекурсивный и не такой красивый
     * Мы можем буквально идти и генерировать строку из скобок, 
     * запоминая количество открытых и закрытых, чтобы всегда успеть завершиться
     */

    private static IEnumerable<string> GenerateParentheses(string s, int maxLen, int openCount, int closedCount)
    {
        if (s.Length == maxLen)
        {
            yield return s;
            yield break;
        }

        var canAddClose = openCount > closedCount;
        var canAddOpen = openCount * 2 < maxLen;

        if (canAddOpen)
        {
            foreach (var s1 in GenerateParentheses(s + '(', maxLen, openCount + 1, closedCount))
            {
                yield return s1;
            }
        }

        if (canAddClose)
        {
            foreach (var s1 in GenerateParentheses(s + ')', maxLen, openCount, closedCount + 1))
            {
                yield return s1;
            }
        }
    }
}
