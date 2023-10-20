using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
// Гороскопы (Наибольшая общая подпоследовательность НОП)
namespace Task_K
{
    /*
    В мире последовательностей нет гороскопов. 
    Поэтому когда две последовательности хотят понять, могут ли они счастливо жить вместе, 
    они оценивают свою совместимость как длину их наибольшей общей подпоследовательности.
    Подпоследовательность получается из последовательности удалением некоторого (возможно, нулевого) числа элементов.
    То есть элементы сохраняют свой относительный порядок, но не обязаны изначально идти подряд.
    Найдите наибольшую общую подпоследовательность двух одиноких последовательностей и выведите её!

    Формат вывода
    Сначала выведите длину найденной наибольшей общей подпоследовательности,
    во второй строке выведите индексы элементов первой последовательности,
    которые в ней участвуют, в третьей строке — индексы элементов второй последовательности. 
    Нумерация индексов с единицы, индексы должны идти в корректном порядке.
    Если возможных НОП несколько, то выведите любую.
     */
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var N = int.Parse(_reader?.ReadLine()); // количество элементов в первой последовательности 
            var iSequence = ReadList(); // элементы первой последовательности
            var M = int.Parse(_reader?.ReadLine()); // количество элементов второй последовательности 
            var jSequence = ReadList(); // элементы второй последовательности

            var dp = CalculateDP(N, iSequence, M, jSequence);
            var answer = FillAnswer(N, iSequence, M, jSequence, dp);

            var first = "";
            var second = "";

            while (answer.Count > 0)
            {
                var (_, id, jd) = answer.Pop();
                first += $"{id} ";
                second += $"{jd} ";
            }

            Console.WriteLine(dp[N, M]); // выведите длину найденной наибольшей общей подпоследовательности
            Console.WriteLine(first); // выведите индексы элементов первой последовательности,
            Console.WriteLine(second);
        }

        private static int[,] CalculateDP(int N, int[] iSequence, int M, int[] jSequence)
        {
            // В dp[i][j] будем хранить длину НОП для подстрок a[1:i] и b[1:j].Такие подстроки называются префиксами.
            var dp = new int[N + 1, M + 1];

            for (var i = 0; i <= N; i++)
            {
                // Каким будет порядок вычисления данных в массиве dp? Будем сначала считать, что строка
                // a состоит из одного символа, а строка b постепенно удлиняется.
                // Потом удлиняем строку a и снова перебираем все возможные длины b.
                for (var j = 0; j <= M; j++)
                {
                    // Каким будет базовый случай для задачи? 
                    if (i == 0 || j == 0)
                    {
                        // Если какая-то из строк пустая, то есть i = 0 или j = 0, то их НОП равна 0: dp[i][j] = 0.
                        dp[i, j] = 0;
                    }
                    else
                    {
                        // Каким будет переход динамики? В процессе перехода мы удлиняем одну из строк на один символ.
                        // Если до удлинения крайние символы в двух строках были равны и включены в НОП,
                        // то добавление нового символа не изменит длину НОП.
                        // Иначе надо сравнить, не совпал ли новый символ с символом на конце второй строки.
                        // Если совпал, то увеличиваем НОП.

                        if (iSequence[i - 1] == jSequence[j - 1])
                        {
                            dp[i, j] = dp[i - 1, j - 1] + 1;
                        }
                        else
                        {
                            dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                        }
                    }
                }
            }

            return dp;
        }

        /// <summary>
        /// Восстановление ответа
        /// </summary>
        private static Stack<(int l, int i, int j)> FillAnswer(int N, int[] iSequence, int M, int[] jSequence, int[,] dp)
        {
            // Завести массив answer, в котором будет храниться НОП, записанная от конца к началу.
            var answer = new Stack<(int l, int i, int j)>();
            var i = N;
            var j = M;
            // Начать с клетки dp[N][M].Приравнять i к N, j к M.

            while (dp[i, j] != 0)
            {
                if (iSequence[i - 1] == jSequence[j - 1])
                {
                    // Если A[i] = B[j], нужно записать в answer символ A[i] и переместиться в клетку dp[i−1][j−1].
                    // Как мы говорили в начале урока, рассмотренный символ точно является частью НОП.
                    answer.Push((iSequence[i - 1], i, j));
                    i--;
                    j--;
                }
                else
                {
                    // Если A[i] != B[j], то один из символов в строках точно не входит в НОП.
                    if (dp[i, j] == dp[i - 1, j])
                    {
                        // значит, существует НОП, в которую A[i] точно не входит.Перемещаемся вверх — в соседнюю ячейку.
                        i--;
                    }
                    else if (dp[i, j] == dp[i, j - 1])
                    {
                        // значит, существует НОП, в которую B[j] не входит. Перемещаемся в ячейку, расположенную левее текущей.
                        j--;
                    }
                }
            }

            return answer;
        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();

    }
}