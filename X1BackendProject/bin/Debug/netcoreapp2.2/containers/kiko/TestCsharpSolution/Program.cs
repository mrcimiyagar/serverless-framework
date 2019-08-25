using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TestCsharpSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                Thread.Sleep(10000);
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.Database.EnsureCreated();
                    Console.WriteLine("Database created !");
                    Console.WriteLine("Hello Keyhan !");
                }                
            }).Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}