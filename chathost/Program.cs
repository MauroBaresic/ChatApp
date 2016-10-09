using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Business;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ChatService)))
            {
                host.Open();

                var address = "";//
                Console.WriteLine($"Up and running on {address}");

                while (true)
                {
                    Console.Write("Enter q to quit: ");
                    var input = Console.ReadLine();
                    if (input?.ToLower() == "q") break;
                }

                Console.WriteLine("Please wait, closing ...");
                host.Close();
            }
        }
    }
}
