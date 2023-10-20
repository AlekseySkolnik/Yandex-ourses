using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Section_2;

/*
Дана скобочная последовательность. Нужно определить, правильная ли она.
Будем придерживаться такого определения:
пустая строка — правильная скобочная последовательность;
правильная скобочная последовательность, взятая в скобки одного типа, – правильная скобочная последовательность;
правильная скобочная последовательность с приписанной слева или справа правильной скобочной последовательностью — тоже правильная.
На вход подаётся последовательность из скобок трёх видов: [], (), {}.
Напишите функцию is_correct_bracket_seq, которая принимает на вход скобочную последовательность и возвращает True,
если последовательность правильная, а иначе False.
 */

public class Task_H
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
    private static StreamWriter _writer = new StreamWriter(Console.OpenStandardOutput());

    public static void Main(string[] args)
    {
        var symbols = ReadList();
        var stack = new Stack<char>();
        var result = true;

        for (int i = 0; i < symbols.Length; i++)
        {
            if (IsOpen(symbols[i]))
            {
                stack.Push(symbols[i]);
            }

            if (IsClose(symbols[i]))
            {
                if (stack.Count == 0)
                {
                    result = false;
                    break;
                }

                if (symbols[i] != Rotate(stack.Pop()))
                {
                    result = false;
                    break;
                }
            }
        }

        result = result && stack.Count == 0;

        _writer.WriteLine(result.ToString());
        _reader.Close();
        _writer.Close();
    }

    private static bool IsOpen(char s) =>
        s == '{' || s == '[' || s == '(';

    private static bool IsClose(char s) =>
        s == '}' || s == ']' || s == ')';

    private static char Rotate(char symbol) =>
        symbol switch
        {
            '{' => '}',
            '[' => ']',
            '(' => ')',
            _ => ' ',
        };

    private static char[] ReadList() =>
        _reader.ReadLine().ToCharArray();
}