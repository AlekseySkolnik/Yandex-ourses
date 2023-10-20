using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;

namespace Section_2;


public class Task_K
{
    public static void Main(string[] args)
    {
        var reader = new StreamReader(Console.OpenStandardInput());
        var n = short.Parse(reader.ReadLine());
        Console.WriteLine(Get_F(n));
    }

    private static int Get_F(short n)
    {
        if (n < 2)
        {
            return 1;
        }

        return Get_F((short)(n - 1)) + Get_F((short)(n - 2));
    }
}
