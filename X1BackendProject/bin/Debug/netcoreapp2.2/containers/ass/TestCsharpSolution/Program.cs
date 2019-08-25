using System;
using System.IO;
using System.Threading.Tasks;

namespace TestCsharpSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await Task.Delay(10000);
                using (var dbContext = new DatabaseContext())
                {
                    dbContext.Database.EnsureCreated();
                    Console.WriteLine("Database created !");
                    Console.WriteLine("Hello Keyhan !");
                }                
            });

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}