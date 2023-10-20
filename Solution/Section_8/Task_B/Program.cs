using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
// Пограничный контроль
namespace Task_B
{
    /*
    Представьте, что вы работаете пограничником и постоянно проверяете документы людей по записи из базы.
    При этом допустима ситуация, когда имя человека в базе отличается от имени в паспорте на одну замену,
    одно удаление или одну вставку символа. 
    Если один вариант имени может быть получен из другого удалением одного символа, 
    то человека пропустят через границу. 
    А вот если есть какое-либо второе изменение, то человек грустно поедет домой или в посольство.
    Например, если первый вариант —– это «Лена», а второй — «Лера», то девушку пропустят. 
    Также человека пропустят, если в базе записано «Коля», а в паспорте — «оля».
    Однако вариант, когда в базе числится «Иннокентий», а в паспорте написано «ннакентий», уже не сработает.
    Не пропустят также человека, у которого в паспорте записан «Иинннокентий»,
    а вот «Инннокентий» спокойно пересечёт границу.
    Напишите программу, которая сравнивает имя в базе с именем в паспорте и решает, пропускать человека или нет.
    В случае равенства двух строк — путешественника, естественно, пропускают.
     */

    internal class Program
    {
        private static StreamReader _reader = new StreamReader(Console.OpenStandardInput());

        static void Main(string[] args)
        {
            var passportName = _reader?.ReadLine() ?? throw new InvalidDataException();
            var databaseName = _reader?.ReadLine() ?? throw new InvalidDataException();

            // на одну замену, одно удаление или одну вставку символа.

            if (passportName.Length >= databaseName.Length)
            {
                (passportName, databaseName) = (databaseName, passportName);
            }

            var result = CompareNames(passportName, databaseName);

            Console.WriteLine(result ? "OK" : "FAIL");
        }

        private static bool CompareNames(string lessStr, string moreStr)
        {
            if (moreStr.Length - lessStr.Length > 1)
            {
                return false;
            }

            if (moreStr.Length - lessStr.Length == 0)
            {
                return CompareLetter(moreStr, lessStr);
            }

            return Compare2Letter(moreStr, lessStr);

        }

        private static bool CompareLetter(string moreStr, string lessStr)
        {
            var changeCount = 0;

            for (int i = 0; i < moreStr.Length; i++)
            {
                if (moreStr[i] != lessStr[i])
                {
                    changeCount++;
                }

                if (changeCount > 1)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Compare2Letter(string moreStr, string lessStr)
        {
            var changeCount = 0;
            var j = 0;

            for (int i = 0; i < lessStr.Length; i++)
            {
                if (moreStr[j] == lessStr[i])
                {
                    j++;
                    continue;
                }

                changeCount++;

                if (moreStr[++j] != lessStr[i])
                {
                    changeCount++;
                }

                if (changeCount > 1)
                {
                    return false;
                }

                j++;
            }

            return changeCount <= 1;
        }
    }
}