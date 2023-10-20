using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Печеньки
namespace Task_D;
/*
К Васе в гости пришли одноклассники. Его мама решила угостить ребят печеньем.
Но не всё так просто. Печенья могут быть разного размера.
А у каждого ребёнка есть фактор жадности —– минимальный размер печенья, которое он возьмёт.
Нужно выяснить, сколько ребят останутся довольными в лучшем случае, когда они действуют оптимально.

Каждый ребёнок может взять не больше одного печенья.

Формат ввода
В первой строке записано n —– количество детей.
Во второй —– n чисел, разделённых пробелом, каждое из которых –— фактор жадности ребёнка. 
Это натуральные числа, не превосходящие 1000.
В следующей строке записано число m –— количество печенек.
Далее —– m натуральных чисел, разделённых пробелом —– размеры печенек. 
Размеры печенек не превосходят 1000.
Оба числа n и m не превосходят 10000.

Формат вывода
Нужно вывести одно число –— количество детей, которые останутся довольными
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        _ = int.Parse(_reader.ReadLine());
        var childrenGreediness = ReadList();
        _ = int.Parse(_reader.ReadLine());
        var cookiesSizes = ReadList();

        Array.Sort(childrenGreediness);
        Array.Sort(cookiesSizes);

        var result = 0;
        var j = 0;

        foreach (var size in cookiesSizes)
        {
            if (j > childrenGreediness.Length - 1)
            {
                break;
            }

            if (size >= childrenGreediness[j])
            {
                result++;
                j++;
            }
        }

        Console.WriteLine(result);
    }

    private static int[] ReadList() =>
        _reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x.ToString()))
        .ToArray();
}
