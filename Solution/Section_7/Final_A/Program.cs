using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
// Расстояние по Левенштейну
namespace Section_7
{
    /*
    Задание
    Расстоянием Левенштейна между двумя строками s и t называется количество атомарных изменений,
    с помощью которых можно одну строку превратить в другую. 
    Под атомарными изменениями подразумеваются: у
     - даление одного символа, 
     - вставка одного символа, 
     - замена одного символа на другой.
    Найдите расстояние Левенштейна для предложенной пары строк.
    Выведите единственное число — расстояние между строками.

    Формат ввода
    В первой строке дана строка s, во второй — строка t.
    Длины обеих строк не превосходят 1000. 
    Строки состоят из маленьких латинских букв.
   */

    /*
    -- ПРИНЦИП РАБОТЫ --
        Рассмотрим на примере строк original = "abсd" и template = "aba".

     j =   0 1 2 3 4
           - a b c d
   i = 0 - 0 1 2 3 4
   i = 1 a 1
   i = 2 b 2
   i = 3 a 3
   
    dp[0][0] = 0. Если обе строки пустые, то расстояние Левенштейна = 0.
    Если строка template пустая (i = 0), а другая состоит из j символов, то dp[0, j] = j.
    Если строка original пустая (j = 0), а другая состоит из i символов, то dp[i, 0] = i.

    Допустим template состоит "a", т.е. i = 1.
    template = "a", original = "" (j = 0), то dist[1,0] = 1.
    template = "a", original = "a", то dist[1,1] = 0. Если символ был бы другой - dist[1,1] = 1.
    template = "a", original = "ab", то dist[1,2] = 1. Атомарная операция удаления/вставки 'b'.
    template = "a", original = "abc", то dist[1,3] = 2. Атомарные операции удаления/вставки 'a' и 'b'.
    template = "a", original = "abсd", то dist[1,4] = 3. Атомарные операции удаления/вставки 'a', 'b', 'c'.

    dp[1, j] = 1 0 1 2 3

     j =   0 1 2 3 4
           - a b a c
   i = 0 - 0 1 2 3 4
   i = 1 a 1 0 1 2 3
   i = 2 b 2
   i = 3 a 3

    Допустим template состоит только из "ab", т.е. i = 2.
    template = "ab", original = "" (j = 0), то dist[2,0] = 2. Атомарные операции удаления/вставки 'a' и 'b'.
    template = "ab", original = "a", то dist[2,1] = 1. Атомарная операция удаления/вставки 'b'.
    template = "ab", original = "ab", то dist[2,2] = 0. Строки равны.
    template = "ab", original = "abc", то dist[2,3] = 1. Атомарная операция удаления/вставки 'c'.
    template = "ab", original = "abсd", то dist[2,4] = 2. Атомарные операции удаления/вставки 'c' и 'd'.
   
    dp[2, j] = 2 1 0 1 2

     j =   0 1 2 3 4
           - a b a c
   i = 0 - 0 1 2 3 4
   i = 1 a 1 0 1 2 3
   i = 2 b 2 1 0 1 2
   i = 3 a 3

    
    Допустим template состоит "aba", т.е. i = 3.
    template = "aba", original = "" (j = 0), то dist[3,0] = 3. Атомарные операции удаления/вставки 'a', 'b', 'a'.
    template = "aba", original = "a", то dist[3,1] = 2. Атомарные операции удаления/вставки 'a' и 'b'.операция удаления/вставки 'b'.
    template = "aba", original = "ab", то dist[3,2] = 1. Атомарная операция удаления/вставки 'a'.
    template = "aba", original = "abc", то dist[3,3] = 1. Атомарная операция замены 'a' -> 'c'.
    template = "aba", original = "abсd", то dist[2,4] = 2. Атомарная операция замены 'a' -> 'c' + aтомарная операция удаления/вставки 'd'.

    dp[3, j] = 2 1 0 1 2

     j =   0 1 2 3 4
           - a b a c
   i = 0 - 0 1 2 3 4
   i = 1 a 1 0 1 2 3
   i = 2 b 2 1 0 1 2
   i = 3 a 3 2 1 1 2

    Обратим внимание, при сравнении строк "aba" и "abc".
    Можно к "ab" добавить 'a' и добавить 'c' - две операции.
    Можно из "aba" удалить 'a' и из "abc" удалить 'c' - две операции.
    Можно просто в "aba" заменить 'a' на 'c' - одна операция.
    В данном случае расстояние Левенштейна опеределяется минимальным набором действий и равно 1.
    dp[i - 1, j - 1] - расстояние Левенштейна для строк с длиной на 1 меньше.
    Если символы original[i] != template[j] не равны, то нужна замена, поэтому change = dp[i - 1, j - 1] + 1;

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
        Что будет храниться в dp?
            dp — это матрица, dp[i][j] — минимальное число атомарных изменений между подстроками original[0..i] и template[0..j].

        Каким будет базовый случай для задачи? 
            dp[0][0] = 0, dp[0, j] = j, dp[i, 0] = i. Нулевой столбец = i. Нулевая строка = j. 
            Это случай, когда original или template состоит из одного символа.

        Каким будет переход динамики?
            Смещение на одну клетку вправо в строке - обратная операция удаления символа, delete = dp[i, j - 1] + 1;
            Смещение на одну клетку вниз - операция вставки символа, add = dp[i - 1, j] + 1;
            Смещение по диагонали на один шаг (i+1, j+1) - операция замены элемента. 
            Если символы original[i] != template[j] не равны, change = dp[i - 1, j - 1] + 1;
            Итоговая формула : dp[i, j] = Min(add, delete, change).

        Каким будет порядок вычисления данных в массиве dp? 
            Заполняем сначала целиком первую строку матрицы, потом вторую и так далее — пока не заполним i-ю строку.

        Где будет располагаться ответ на исходный вопрос? 
            Ответ будет лежать в правом нижнем углу — в ячейке dp[N][M].

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
		N - длина первой строки, M - длина второй строки.
		Скорость алгоритма зависит от длины строк.Чем больше строки, тем дольше будет выполняться алгоритм.
		Мы имеем 2 вложенных цикла, внутри которых заполняем каждую ячейку dp[i, j]. Это будет сделано NxM раз.
		Поэтому временная сложность - O(N*M).

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
        O(N*M) - потому что храним матрицу dp размером [n + 1, m + 1];
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/25597/run-report/91907155/
    /// </summary>
    public class Final_A
    {
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main(string[] args)
        {
            var original = _reader?.ReadLine() ?? string.Empty;
            var template = _reader?.ReadLine() ?? string.Empty;
            var dist = GetLevenshteinDistance(original, template);
            Console.WriteLine(dist);
        }

        public static double GetLevenshteinDistance(string original, string template)
        {
            var n = original.Length;
            var m = template.Length;
            var dp = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[0, j] = j;
                        dp[i, 0] = i;
                    }
                    else
                    {
                        var isEqualSymbol = original[i - 1] == template[j - 1];
                        var delete = dp[i, j - 1] + 1; // current row
                        var add = dp[i - 1, j] + 1; // previous row
                        var change = dp[i - 1, j - 1] + (isEqualSymbol ? 0 : 1); // previous row
                        dp[i, j] = new List<int> { add, delete, change }.Min(); // current row
                    }
                }
            }

            return dp[n, m];
        }
    }
}