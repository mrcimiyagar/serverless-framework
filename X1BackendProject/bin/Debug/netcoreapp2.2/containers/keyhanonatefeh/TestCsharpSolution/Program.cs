using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TestCsharpSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoLayer.Setup();
            
            using (var mongoLayer = new MongoLayer())
            {
                mongoLayer.GetNotifsColl().InsertOne(BsonDocument.Parse("{ hello: 'world' }"));
                Console.WriteLine("Database created !");
                Console.WriteLine("Hello Keyhan !");
            }

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}