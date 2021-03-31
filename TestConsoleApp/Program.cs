using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var apm = new iHawkIISLibrary.ApplicationPoolsManager())
            {
                var list = apm.GetApplicationPoolList();
                foreach (var pool in list)
                {
                    Console.Out.WriteLine(pool);
                }
            }

            Console.ReadLine();
        }
    }
}
