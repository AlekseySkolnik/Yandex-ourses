using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Сравнить две строки
namespace Task_K
{
    /*
    Алла придумала новый способ сравнивать две строки: 
    чтобы сравнить строки a и b, в них надо оставить только те буквы, 
    которые в английском алфавите стоят на четных позициях. 
    Затем полученные строки сравниваются по обычным правилам. 
    Помогите Алле реализовать новое сравнение строк.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var text = _reader?.ReadLine() ?? throw new InvalidDataException();
            var template = _reader?.ReadLine() ?? throw new InvalidDataException();

            var newText = new List<char>();
            var newTemplate = new List<char>();

            foreach (char c in text)
            {
                if (c % 2 == 0)
                {
                    newText.Add(c);
                }
            }

            foreach (char c in template)
            {
                if (c % 2 == 0)
                {
                    newTemplate.Add(c);
                }
            }

            text = string.Concat(newText);
            template = string.Concat(newTemplate);

            Console.WriteLine(string.Compare(text, template));

        }
    }
}