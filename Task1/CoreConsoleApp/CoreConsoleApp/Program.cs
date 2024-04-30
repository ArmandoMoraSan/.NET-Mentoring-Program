using System;
using Task2;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.WriteLine(CurrentTimeUser.SetCurrentTimeUser(username));
        }
    }
}
