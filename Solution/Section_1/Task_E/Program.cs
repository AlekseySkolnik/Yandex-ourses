using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Section_1;

/*
Чтобы подготовиться к семинару, Гоше надо прочитать статью по эффективному менеджменту. 
Так как Гоша хочет спланировать день заранее, ему необходимо оценить сложность статьи.
Он придумал такой метод оценки: берётся случайное предложение из текста и в нём ищется самое длинное слово.
Его длина и будет условной сложностью статьи.
Помогите Гоше справиться с этой задачей.

Формат ввода
В первой строке дана длина текста L (1 ≤ L ≤ 105).

В следующей строке записан текст, состоящий из строчных латинских букв и пробелов. 
Слово —– последовательность букв, не разделённых пробелами. 
Пробелы могут стоять в самом начале строки и в самом её конце. 
Текст заканчивается переносом строки, этот символ не включается в число остальных L символов.

Формат вывода
В первой строке выведите самое длинное слово. 
Во второй строке выведите его длину. 
Если подходящих слов несколько, выведите то, которое встречается раньше.
 */
public class Task_E
{
    private static TextReader _reader;
    private static TextWriter _writer;

    public static void Main(string[] args)
    {
        _reader = new StreamReader(Console.OpenStandardInput());
        _writer = new StreamWriter(Console.OpenStandardOutput());
        _ = ReadInt();
        var inputStrings = ReadStringList();

        var maxNumber = 0;
        var maxLength = inputStrings[maxNumber].Length;

        for (int i = 1; i < inputStrings.Count; i++)
        {
            if (inputStrings[i].Length > maxLength)
            {
                maxNumber = i;
                maxLength = inputStrings[i].Length;
            }
        }

        _writer.WriteLine(inputStrings[maxNumber]);
        _writer.WriteLine(maxLength);

        _reader.Close();
        _writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(_reader.ReadLine());
    }

    private static List<string> ReadStringList() =>
        _reader.ReadLine()
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .ToList();
}