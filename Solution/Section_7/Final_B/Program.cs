using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
// Одинаковые суммы
namespace Section_7
{
    /*
    Задание
    На Алгосах устроили турнир по настольному теннису.
    Гоша выиграл n партий, получив при этом некоторое количество очков за каждую из них.
    Гоше стало интересно, можно ли разбить все заработанные им во время турнира очки на две части так,
    чтобы сумма в них была одинаковой.
    Формат вывода
    Нужно вывести True, если произвести такое разбиение возможно, иначе – False.
   */

    /*
    -- ПРИНЦИП РАБОТЫ --

    Представим вход алгоритма как список вида: S=x1, ..., xn
    Пусть N — число элементов в множестве S.
    Пусть K — сумма всех элементов из множества S.
    То есть K=x1 + ... + xn. 
    Мы построим алгоритм, который определяет, существует ли подмножество S, сумма элементов которого равна |K/2|.
    Если подмножество существует, то:
        - если K чётно, то остаток множества S также даст |K/2|;
        - если K нечётно, остаток множества S даст сумму |K/2|;

    Алгоритм строит таблицу размера |K/2| на N, содержащую значения рекурсии, 
        где K — сумма значений, а n — число элементов. 
    Когда вся таблица заполнена, возвращаем dp[|K/2|,n].

    dp[i, j] принимает значение True, если либо p[i, j − 1] = True, либо dp[i − X[j], j − 1] = True;
    dp[i, j] принимает значение False в противном случае.

    В итоге в ячейке получается true, либо если i = X[j], то есть число X[j] равно текущей сумме i,
    либо если нескольких чисел от X[0] до X[j] дает в сумме i.

    В последней строке как раз будет информация, есть ли набор чисел, дающий в сумме полусумму начальной последовательности.
    Если есть, значит исходную последовательность можно разбить на две, суммы которых одинаковы.

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

    Доказательство https://ru.wikipedia.org/wiki/Задача_разбиения_множества_чисел

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    Мы имеем 2 вложенных цикла, внутри которых заполняем каждую ячейку dp[i, j]. Это будет сделано K/2xN раз.
    Поэтому временная сложность - O(N*K).

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    Алгоритм требует выделения памяти под таблицу размера |K/2| на N, содержащую значения рекурсии, где K — сумма значений, а n — число элементов.
    Поэтому сложность O(N*K).
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/25597/run-report/92233317/
    /// </summary>
    public class Final_B
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main(string[] args)
        {
            var n = int.Parse(_reader?.ReadLine());
            var jSum = ReadList(n);

            var result = IsPossibleToSplit(jSum, n);

            Console.WriteLine(result.ToString());
        }

        private static bool IsPossibleToSplit(List<int> jSum, int n)
        {
            var K = jSum.Sum();

            // Если сумма нечетна, то сразу возвращаем false, т.к. невозможно ее поделить пополам без остатка
            if (K % 2 != 0)
            {
                return false;
            }

            var iMax = K / 2 + (K / 2 % 2 == 0 ? 0 : 1);
            var jMax = n + 1;

            var dp = new bool[iMax, jMax];

            for (var j = 0; j < jMax; j++)
            {
                // инициализируем верхнюю строку (P(0,x)) таблицы dp значениями True
                dp[0, j] = true;
            }

            for (var i = 1; i < iMax; i++)
            {
                for (var j = 0; j < jMax; j++)
                {
                    if (j == 0)
                    {
                        // инициализируем крайний левый столбец (P(x, 0)) таблицы dp значениями False
                        dp[i, 0] = false;
                    }
                    else
                    {
                        var result = dp[i, j - 1];

                        if (i - jSum[j - 1] >= 0)
                        {
                            result = result || dp[i - jSum[j - 1], j - 1];
                        }

                        dp[i, j] = result;
                    }
                }
            }

            return dp[iMax - 1, jMax - 1];
        }

        private static List<int> ReadList(int take) =>
            _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Take(take)
            .ToList() ?? new List<int>();
    }
}