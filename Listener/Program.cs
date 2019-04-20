using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listener
{
    class Program
    {
        private static string topic = "weather-topic";
        private static string subscription = "listener";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----   This is a listener   -----");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.ForegroundColor = ConsoleColor.Gray;

            var serviceBusConnectionString = ConfigurationManager.AppSettings["serviceBus"];
            var subscriptionClient = new SubscriptionClient(serviceBusConnectionString, topic, subscription);
            subscriptionClient.RegisterMessageHandler(async (msg, cancelationToken) =>
            {
                var body = Encoding.UTF8.GetString(msg.Body);
                Console.WriteLine(body);

                await Task.CompletedTask;
            },
            async exception =>
            {
                await Task.CompletedTask;
                // log exception
            }
            );

            Console.ReadLine();
        }
    }
}
