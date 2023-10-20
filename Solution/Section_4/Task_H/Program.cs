using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Странное сравнение
namespace Task_H;
/*
Жители Алгосского архипелага придумали новый способ сравнения строк. 
Две строки считаются равными, если символы одной из них можно заменить на символы другой так,
что первая строка станет точной копией второй строки.
При этом необходимо соблюдение двух условий:

Порядок вхождения символов должен быть сохранён.
Одинаковым символам первой строки должны соответствовать одинаковые символы второй строки.
Разным символам —– разные.
Например, если строка s = «abacaba», то ей будет равна строка t = «xhxixhx»,
так как все вхождения «a» заменены на «x», «b» –— на «h», а «c» –— на «i».
Если же первая строка s=«abc», а вторая t=«aaa», то строки уже не будут равны, 
так как разные буквы первой строки соответствуют одинаковым буквам второй.

Формат ввода
В первой строке записана строка s, во второй –— строка t.
Длины обеих строк не превосходят 106.
Обе строки содержат хотя бы по одному символу и состоят только из маленьких латинских букв.

Строки могут быть разной длины.

Формат вывода
Выведите «YES», если строки равны (согласно вышеописанным правилам), и «NO» в ином случае.
 */

internal class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

    static void Main(string[] args)
    {
        var firstStr = _reader.ReadLine() ?? string.Empty;
        var seconStr = _reader.ReadLine() ?? string.Empty;

        SecondVariant(firstStr, seconStr);
    }

    private static void FirstVariant(string firstStr, string seconStr)
    {
        if (firstStr.Length != seconStr.Length)
        {
            Console.WriteLine("NO");
        }
        else
        {
            var count = firstStr.Length;

            var dict = new Dictionary<char, char>()
            {
                { firstStr[0], seconStr[0] }
            };

            var result = true;

            for (int i = 1; i < count; i++)
            {
                if (!dict.ContainsKey(firstStr[i]))
                {
                    if (dict.ContainsValue(seconStr[i]))
                    {
                        result = false;
                        break;
                    }

                    dict.Add(firstStr[i], seconStr[i]);
                    continue;
                }

                if (dict[firstStr[i]] != seconStr[i])
                {
                    result = false;
                    break;
                }
            }

            var message = result ? "YES" : "NO";
            Console.WriteLine(message);
        }
    }

    private static void SecondVariant(string firstStr, string seconStr)
    {

        if (firstStr.Length != seconStr.Length)
        {
            Console.WriteLine("NO");
        }
        else
        {
            var count = firstStr.Length;

            var dict = new Dictionary<char, char>()
            {
                { firstStr[0], seconStr[0] }
            };

            var bit = new Bit();
            bit.Set(seconStr[0]);
            var result = true;

            for (int i = 1; i < count; i++)
            {
                if (!dict.ContainsKey(firstStr[i]))
                {
                    if (bit.Get(seconStr[i]))
                    {
                        result = false;
                        break;
                    }

                    dict.Add(firstStr[i], seconStr[i]);
                    bit.Set(seconStr[i]);
                    continue;
                }

                if (dict[firstStr[i]] != seconStr[i])
                {
                    result = false;
                    break;
                }
            }

            var message = result ? "YES" : "NO";
            Console.WriteLine(message);
        }
    }
}

public class Bit
{
    private readonly bool[] _flags = new bool[26];

    public void Set(char v)
    {
        _flags[v - 'a'] = true;
    }

    public bool Get(char v) =>
        _flags[v - 'a'];
}