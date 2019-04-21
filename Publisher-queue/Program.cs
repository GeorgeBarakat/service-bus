using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.ServiceBus;


namespace Publisher_queue
{
    class Program
    {
        //static string serviceBus = ConfigurationManager.AppSettings["serviceBus"];
        //static string queueName = ConfigurationManager.AppSettings["queueName"];

        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("All parameters should be provided before sending.");
                Console.WriteLine("Please enter parameters as the following \"End-point\" \"Queue Name\" \"Message Content\". ");
                Environment.Exit(0);
            }
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            string[] messages = args.Skip(2).ToArray();
            int numberOfMessages = args.Count(s => s != null);
            queueClient = new QueueClient(args[0], args[1]);

            Console.WriteLine("=======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("=======================================================");

            // Send Messages
            await SendMessagesAsync(messages, numberOfMessages - 2);

            Console.WriteLine("All messages has been sent.");
            Console.ReadKey();

            await queueClient.CloseAsync();

            
        }

        static async Task SendMessagesAsync(string[] messages, int numberOfMessagesToSend)
        {
            try
            {
                foreach (string message in messages)
                {
                    var messageToSend = new Message(Encoding.UTF8.GetBytes(message));

                    // Write the body of the message to the console
                    Console.WriteLine(String.Format("Sending Message \"{0}\"", message));

                    // Send the message to the queue
                    await queueClient.SendAsync(messageToSend);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("{0} :: Exception: {1}", DateTime.Now, exception.Message));
            }
        }
    }
}
