using System;
using System.IO;

namespace TestCsharpSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DatabaseContext())
            {
                dbContext.Database.EnsureCreated();
                Console.WriteLine("Database created !");
                Console.WriteLine("Hello Keyhan !");
            }
        }
    }
}