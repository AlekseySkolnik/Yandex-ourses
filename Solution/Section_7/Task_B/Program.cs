using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Расписание
namespace Task_B
{
    /*
    Дано количество учебных занятий, проходящих в одной аудитории.
    Для каждого из них указано время начала и конца. 
    Нужно составить расписание, в соответствии с которым в классе можно будет провести 
    как можно больше занятий.
    Если возможно несколько оптимальных вариантов, то выведите любой. 
    Возможно одновременное проведение более чем одного занятия нулевой длительности.

    Формат ввода
   
    Формат вывода
     */

    //  Попробуй выбирать мероприятие, которое ещё не началось, но закончится раньше остальных.
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var count = int.Parse(_reader?.ReadLine());
            var times = new (double start, double end)[count];

            for (var i = 0; i < count; i++)
            {
                var lessons = _reader?.ReadLine()?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries);
                double.TryParse(lessons[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var start);
                double.TryParse(lessons[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var end);
                times[i] = (start, end);
            }

            Array.Sort(times, (a, b) =>
            {
                if (a.end != b.end)
                {
                    return (int)(a.end - b.end);
                }

                return (int)(a.start - b.start);
            });

            var outputTimes = new List<(double start, double end)>();
            double endTime = 0;
            (double start, double end)? current;

            for (var i = 0; i < count; i++)
            {
                if (times[i].start < endTime)
                {
                    continue;
                }
                else
                {
                    outputTimes.Add(times[i]);
                    endTime = times[i].end;
                }
            }

            Console.WriteLine(outputTimes.Count);
            outputTimes.ForEach(x => Console.WriteLine($"{x.start} {x.end}"));
        }
    }
}