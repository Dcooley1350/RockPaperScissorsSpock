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

        public static int GetUserNum()
        {
            string numString = Console.ReadLine();
            int output;
            bool isSuccessful = int.TryParse(numString,out output);

            if(!isSuccessful)
            {
                output = -1;
            }
            else
            {
                output = Math.Abs(output);
            }
            return output;
        }

    }
}