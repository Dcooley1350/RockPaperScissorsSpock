using System;
using System.Collections.Generic;
using UI;
using Rps.Models;

class Program
{
    static void Main()
    {
        int players = AskForNumber("How many players?", "Please enter the number of players.");

        int rounds = AskForNumber("How many rounds?", "Please enter the number of rounds.");
        
        Game game = new Game(rounds, players);
        for(int i = 0; i<game.Rounds; i++)
        {
            game.PlayRound();
            List<string> overallWinner = game.OverAllWinner();
            if(overallWinner.Count > 0)
            {
                break;
            }
        }

        Console.WriteLine("Game Over!");
        Console.WriteLine(string.Join(",", game.OverAllWinner().ToArray()));

    }

    public static int AskForNumber(string message, string errorMsg)
    {
        int value = -1;
        Console.WriteLine(message);
        while(value == -1)
        {
            value = Interface.GetUserNum();
            if(value == -1)
            {
                Console.WriteLine(errorMsg);
            }
        }
        return value;
    }

}