using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Золотая лихорадка
namespace Task_C
{
    /*
    Гуляя по одному из островов Алгосского архипелага, Гоша набрёл на пещеру, 
    в которой лежат кучи золотого песка. 
    К счастью, у Гоши есть с собой рюкзак грузоподъёмностью до M килограмм,
    поэтому он может унести с собой какое-то ограниченное количество золота.
    Всего золотых куч n штук, и все они разные.
    В куче под номером i содержится mi килограммов золотого песка,
    а стоимость одного килограмма — ci алгосских франков.
    Помогите Гоше наполнить рюкзак так, 
    чтобы общая стоимость золотого песка в пересчёте на алгосские франки была максимальной.

    Формат ввода
   
    Формат вывода
    Выведите единственное число —– максимальную сумму (в алгосских франках),
    которую Гоша сможет вынести из пещеры в своём рюкзаке.
     */

    //  Попробуй выбирать мероприятие, которое ещё не началось, но закончится раньше остальных.
    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var M = long.Parse(_reader?.ReadLine()); // грузоподъёмность рюкзака Гоши
            var N = int.Parse(_reader?.ReadLine()); // количество куч с золотым песком 
            var heap = new (long price, long weight)[N]; // куча. 
            long sum = 0;

            for (int i = 0; i < N; i++)
            {
                var current = ReadList();
                heap[i] = (current[0], current[1]);
            }

            Array.Sort(heap, (a, b) =>
            {
                return (int)(b.price - a.price);
            });

            for (int i = 0; i < N; i++)
            {
                var dif = M - heap[i].weight;

                if (dif >= 0)
                {
                    sum += heap[i].price * heap[i].weight;
                    M = dif;
                }
                else
                {
                    sum += heap[i].price * M;
                    break;
                }
            }

            Console.WriteLine(sum);
        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();
    }
}