using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// A. Генератор скобок
namespace Task_A;

/*
Рита по поручению Тимофея наводит порядок в правильных скобочных последовательностях (ПСП), 
состоящих только из круглых скобок (). 
Для этого ей надо сгенерировать все ПСП длины 2n в алфавитном порядке —
алфавит состоит из ( и ) и открывающая скобка идёт раньше закрывающей.
Помогите Рите —– напишите программу, которая по заданному n выведет все ПСП в нужном порядке.
Рассмотрим второй пример. Надо вывести ПСП из четырёх символов. Таких всего две:
(())
()()
(()) идёт раньше ()(), так как первый символ у них одинаковый, а на второй позиции у первой ПСП стоит (, который идёт раньше ).
Формат ввода
На вход функция принимает n — целое число от 0 до 10.
Формат вывода
Функция должна напечатать все возможные скобочные последовательности заданной длины в алфавитном (лексикографическом) порядке.
 */
class Program
{
    private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());
    private static List<string> _allCombinations = new List<string>();

    static void Main(string[] args)
    {
        var n = byte.Parse(_reader.ReadLine()); // 0..10

        GenCombinations(2 * n, "");

        foreach (var x in _allCombinations)
        {
            if (IsRightCombination(x))
            {
                Console.WriteLine(x);
            }
        }

        SecondVariant.SolvedParentheses();
    }

    static void GenCombinations(int n, string prefix)
    {
        if (n == 0)
        {
            // Console.WriteLine(prefix);
            _allCombinations.Add(prefix);
        }
        else
        {
            GenCombinations(n - 1, prefix + "(");
            GenCombinations(n - 1, prefix + ")");
        }
    }

    private static bool IsRightCombination(string symbols)
    {
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

        return result;
    }

    private static bool IsOpen(char s) =>
        s == '(';

    private static bool IsClose(char s) =>
        s == ')';

    private static char Rotate(char symbol) =>
        symbol switch
        {
            '(' => ')',
            _ => ' ',
        };
}
