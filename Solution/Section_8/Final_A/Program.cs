using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Packed Prefix
namespace Section_8
{
    /*
    Задание
    Вам даны строки в запакованном виде. 
    Определим запакованную строку (ЗС) рекурсивно.
    Строка, состоящая только из строчных букв английского алфавита является ЗС. 
    Если A и B —– корректные ЗС, то и AB является ЗС.
    Если A —– ЗС, а n — однозначное натуральное число, то n[A] тоже ЗС. 
    При этом запись n[A] означает, что при распаковке строка A записывается подряд n раз.
    Найдите наибольший общий префикс распакованных строк и выведите его (в распакованном виде).

    Формат ввода
    В первой строке записано число n (1 ≤ n ≤ 1000) –— число строк.
    Далее в n строках записаны запакованные строки. 
    Гарантируется, что эти строки корректны, то есть удовлетворяют указанному рекурсивному определению.
    Длина строк после распаковки не превосходит 105.

    Формат вывода
    Выведите наибольший общий префикс распакованных строк.

    -- ПРИНЦИП РАБОТЫ --
    1. Распаковать строку. Присвоить префиксу значение первой строки.
    2. Последовательно читаем строки, распаковываем. Если текущий префикс длиннее строки, то 
       сокращаем его: prefix = prefix[..currentStr.Length].
    3. Сравниваем часть текущей строки (длиной prefix.Length) с префиком, если не равны, 
       удаляем из префикса последний символ. Делаем так, до тех пор пока 
       префикс не станет равен части строки, либо пока не станет пустым.
    4. Если стал пустым, прерываемся - общих префиксов у строк нет и дальше можно не читать.
       Если не пустой, повторяем пункты 2-3.  

    Распаковка строки: предварительно создаем два стека - digitsStack и symbolStack.
    Идем посимвольно по строке:
     - Если текущий symbol цифра - добавляем ее в стек digitsStack. // digitsStack.Push(digit);
     - Если текущий symbol = '[' - добавляем в стек symbolStack новый пустой список. // symbolStack.Push(new List<char>());
     - Если текущий symbol = ']' - смотрим на размер symbolStack:
        - если symbolStack.Count == 1, то забираем из него список символов, вставляем их в результат, дублирая столько раз,
            сколько лежит на вершине стека digitsStack.
        - если symbolStack.Count != 1, то забираем из него список символов и пересохраняем его в следующий список стека symbolStack,
            причем столько раз, сколько лежит на вершине стека digitsStack.
    - Иначе если symbolStack пустой, то добавляем символ в результат, если нет, то добавляем сивол в верхний список стека symbol.

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
	
    Распаковка одной строки O(N), N - длина строки.
    Распаковка всех строк  O(N*count).
    Поиск префикса O(count*N)
    
    Общая O(L), где L — суммарная длина слов во множестве.

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
      Не знаю, как посчитать. Чем больше в строке запакованных блоков, тем больше памяти пондобится под хранение.        
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/26133/run-report/93687272/
    /// </summary>
    public class Final_A
    {
        private const char OpeningBracket = '[';
        private const char ClosingBracket = ']';
        private const char Zero = '0';
        private const string Separator = "";
        private static StreamReader _reader = new(Console.OpenStandardInput());

        public static void Main(string[] args)
        {
            var count = int.Parse(_reader?.ReadLine());
            var prefix = Unpack(_reader?.ReadLine());

            prefix = ProcessingStrings(count, prefix);

            Console.WriteLine(prefix);
        }

        private static string ProcessingStrings(int count, string prefix)
        {
            for (var i = 1; i < count; i++)
            {
                var currentStr = Unpack(_reader?.ReadLine());

                if (prefix.Length > currentStr.Length)
                {
                    prefix = prefix[..currentStr.Length];
                }

                while (!string.IsNullOrEmpty(prefix) && currentStr[..prefix.Length] != prefix)
                {
                    prefix = prefix[..(prefix.Length - 1)];
                }

                if (string.IsNullOrEmpty(prefix))
                {
                    break;
                }
            }

            return prefix;
        }

        private static string Unpack(string str)
        {
            var result = new StringBuilder();
            var digitsStack = new Stack<int>();
            var symbolStack = new Stack<List<char>>();

            foreach (var symbol in str)
            {
                if (symbol == ClosingBracket)
                {
                    if (symbolStack.Count == 1)
                    {
                        var s = string.Join(Separator, symbolStack.Pop());
                        result.Append(string.Join(Separator, Enumerable.Repeat(s, digitsStack.Pop())));
                        continue;
                    }

                    var previous = string.Join(Separator, symbolStack.Pop());
                    var symbols = symbolStack.Peek();

                    foreach (var x in Enumerable.Repeat(previous, digitsStack.Pop()))
                    {
                        symbols.AddRange(x.ToCharArray());
                    }

                    continue;
                }

                if (symbol == OpeningBracket)
                {
                    symbolStack.Push(new List<char>());
                    continue;
                }

                if (char.IsDigit(symbol))
                {
                    int digit = symbol - Zero;
                    digitsStack.Push(digit);
                    continue;
                }

                if (symbolStack.Count == 0)
                {
                    result.Append(symbol);
                    continue;
                }

                symbolStack.Peek().Add(symbol);
            }

            return result.ToString();
        }
    }
}