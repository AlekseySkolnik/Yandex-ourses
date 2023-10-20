using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Разворот строки
namespace Task_E
{
    /*
    У Риты была строка s, Гоша подарил ей на 8 марта ещё n других строк ti, 1≤ i≤ n.
    Теперь Рита думает, куда их лучше поставить. 
    Один из вариантов — расположить подаренные строки внутри имеющейся строки s,
    поставив строку ti сразу после символа строки s с номером ki 
    (в частности, если ki=0, то строка вставляется в самое начало s).
    Помогите Рите и определите, какая строка получится после вставки в s всех подаренных Гошей строк.

     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var s = _reader?.ReadLine() ?? throw new InvalidDataException();
            var n = int.Parse(_reader?.ReadLine());

            string result = TransformSpan(s, n);

            Console.WriteLine(result);
        }

        private static string Transform(string s, int n)
        {
            var tasks = new PriorityQueue<string, int>();

            for (int i = 0; i < n; i++)
            {
                var inputs = _reader?.ReadLine()?.Split(' ');
                var k = int.Parse(inputs[1]);
                tasks.Enqueue(inputs[0], k);
            }

            var builder = new StringBuilder();
            var prevLength = 0;

            while (tasks.Count > 0)
            {
                tasks.TryDequeue(out var t, out var k);
                builder.Append(s[prevLength..k]);
                builder.Append(t);
                prevLength = k;
            }

            builder.Append(s[prevLength..]);
            var result = builder.ToString();
            return result;
        }

        private static string TransformSpan(string s, int n)
        {
            var tasks = new PriorityQueue<string, int>();

            for (int i = 0; i < n; i++)
            {
                var inputs = _reader?.ReadLine()?.Split(' ');
                var k = int.Parse(inputs[1]);
                tasks.Enqueue(inputs[0], k);
            }

            var span = s.AsSpan();

            var builder = new StringBuilder();
            var prevLength = 0;

            while (tasks.Count > 0)
            {
                tasks.TryDequeue(out var t, out var k);
                builder.Append(span[prevLength..k]);
                builder.Append(t);
                prevLength = k;
            }

            builder.Append(span[prevLength..]);
            var result = builder.ToString();
            return result;
        }

        private static string TransformSpan2(string s, int n)
        {
            var tasks = new PriorityQueue<string, int>();

            for (int i = 0; i < n; i++)
            {
                var inputs = _reader.ReadLine().Split(' ').AsSpan();
                var k = int.Parse(inputs[1]);
                tasks.Enqueue(inputs[0], k);
                inputs = null;
            }

            var span = s.AsSpan();

            var builder = new StringBuilder();
            var prevLength = 0;

            while (tasks.Count > 0)
            {
                tasks.TryDequeue(out var t, out var k);
                builder.Append(span[prevLength..k]);
                builder.Append(t);
                prevLength = k;
            }

            builder.Append(span[prevLength..]);
            var result = builder.ToString();
            return result;
        }
    }
}