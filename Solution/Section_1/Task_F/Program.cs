using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Section_1;

/*
Помогите Васе понять, будет ли фраза палиндромом.
Учитываются только буквы и цифры, заглавные и строчные буквы считаются одинаковыми.
Решение должно работать за O(N), где N — длина строки на входе.

Формат ввода
В единственной строке записана фраза или слово. 
Буквы могут быть только латинские.
Длина текста не превосходит 20000 символов.
Фраза может состоять из строчных и прописных латинских букв, цифр, знаков препинания.

Формат вывода
Выведите «True», если фраза является палиндромом, и «False», если не является.
 */
public class Task_F
{
    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());
        char[] inputString = GetString();

        _writer.Write(IsPalindrome(inputString).ToString());
        _reader.Close();
        _writer.Close();
    }

    private static bool IsPalindrome(char[] inputString)
    {
        var isPalindrome = true;
        var left = 0;
        var right = inputString.Length - 1;

        while (left < right)
        {
            if (!char.IsLetterOrDigit(inputString[left]))
            {
                left++;
                continue;
            }

            if (!char.IsLetterOrDigit(inputString[right]))
            {
                right--;
                continue;
            }

            if (inputString[left] != inputString[right])
            {
                isPalindrome = false;
                break;
            }

            left++;
            right--;

        }

        return isPalindrome;
    }

    private static char[] GetString()
    {
        return _reader.ReadLine().ToLower().ToCharArray();
    }
}