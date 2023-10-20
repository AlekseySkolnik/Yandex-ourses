using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Последовательность
namespace Task_C;
/*
Гоша любит играть в игру «Подпоследовательность»: 
даны 2 строки, и нужно понять, является ли первая из них подпоследовательностью второй. 
Когда строки достаточно длинные, очень трудно получить ответ на этот вопрос, просто посмотрев на них. 
Помогите Гоше написать функцию, которая решает эту задачу.

Формат ввода
В первой строке записана строка s.
Во второй —- строка t.
Обе строки состоят из маленьких латинских букв, длины строк не превосходят 150000.
Строки не могут быть пустыми.

Формат вывода
Выведите True, если s является подпоследовательностью t, иначе —– False.
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var sub = _reader.ReadLine() ?? "abc";
        var full = _reader.ReadLine() ?? "ahbgdcu";

        var result = IsSubSequence(full, sub);

        Console.WriteLine(result);
    }

    private static bool IsSubSequence(string full, string sub)
    {
        var i = 0;

        foreach (var symbol in full.ToCharArray())
        {
            if (symbol == sub[i])
            {
                i++;

                if (i == sub.Length)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
