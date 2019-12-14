# nullable enable
using System;

namespace ReignTools.Service
{
    public class ConsoleReaderService : IConsoleReaderService
    {
        public short ReadLine()
        {
            var line = Console.ReadLine();
            if (!short.TryParse(line, out short result) || (result < 2 || result > 10))
            {
                return -1;
            }

            return result;
        }
    }
}
