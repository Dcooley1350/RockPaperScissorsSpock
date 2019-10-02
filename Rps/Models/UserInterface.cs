using System;

namespace UI
{

    public class Interface
    {
        public static string GetUserInput(string playerName)
        {
            Console.WriteLine(playerName + " enter your value:");
            return Console.ReadLine().ToLower();
        }

    }
}