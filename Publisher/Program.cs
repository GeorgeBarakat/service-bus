using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        private static string topic = "weather-topic";
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("All parameters should be provided before sending messages.");
                Console.WriteLine("Please enter parameters as the following \"End-point\" \"Topic Name\". ");
                Environment.Exit(0);
            }

            Console.WriteLine("-----   This is a publisher   -----");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            var loop = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Write you message:");

                var message = Console.ReadLine();
                if (message == "q")
                    break;

                //var serviceBusConnectionString = ConfigurationManager.AppSettings["serviceBus"];
                var topicClient = new TopicClient(args[0], args[1]);
                var body = Encoding.UTF8.GetBytes(message);
                var busMessage = new Message(body);
                topicClient.SendAsync(busMessage).GetAwaiter().GetResult();
                

            } while (loop);


        }
    }
}
