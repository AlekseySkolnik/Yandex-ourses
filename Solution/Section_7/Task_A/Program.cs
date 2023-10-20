using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Биржа
namespace Task_A
{
    /*
    Рита хочет попробовать поиграть на бирже.
    Но для начала она решила потренироваться на исторических данных.
    Даны стоимости акций в каждый из n дней.
    В течение дня цена акции не меняется. 
    Акции можно покупать и продавать, но только по одной штуке в день. 
    В один день нельзя совершать более одной операции (покупки или продажи). 
    Также на руках не может быть более одной акции в каждый момент времени.
    Помогите Рите выяснить, какую максимальную прибыль она могла бы получить.

    Формат ввода
    В первой строке записано количество дней n —– целое число в диапазоне от 0 до 10 000.
    Во второй строке через пробел записано n целых чисел в диапазоне от 0 до 1000 –— цены акций.
    
    Формат вывода
    Выведите число, равное максимально возможной прибыли за эти дни.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var daysCount = int.Parse(_reader?.ReadLine());
            var pricesByDays = ReadList();
            var dif = new int[daysCount - 1];

            for (int i = 0; i < daysCount - 1; i++)
            {
                dif[i] = pricesByDays[i + 1] - pricesByDays[i];
            }

            Console.WriteLine(dif.Where( x => x > 0).Sum());

        }

        private static int[] ReadList() =>
        _reader?.ReadLine()
            ?.Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray() ?? Array.Empty<int>();
    }
}