using System;

namespace SDLog.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = new FileLogger();
            log.Log<Program>("Hello, world");
            Console.Read();
        }
    }
}
