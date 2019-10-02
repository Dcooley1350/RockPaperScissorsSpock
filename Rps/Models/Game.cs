using System;
using System.Collections.Generic;
using UI;
using System.Linq;

namespace Rps.Models
{
    public class Game
    {
        public List<string> ValidValues {get;}
        public List<string> Results {get;}
        public int Rounds {get;}
        public int PlayerCount {get;}
        public Dictionary<string,string[]> WinRules {get;}
        public Game(int roundCount, int playerCount)
        {
            ValidValues = new List<string>{"scissors", "s", "rock", "r", "paper", "p", "spock"};
            Rounds = roundCount;
            PlayerCount = playerCount;
            Results = new List<string>();
            WinRules = new Dictionary<string, string[]>();
            WinRules.Add("r", new string[]{"s"});
            WinRules.Add("p", new string[]{"r"});
            WinRules.Add("s", new string[]{"p"});
            WinRules.Add("spock", new string[]{"r","p","s"});
        }
        public string CheckUserInput(string input)
        {
            input = input.ToLower();
            if(!ValidValues.Contains(input))
            {
                input = "Error";
            }
            else
            {
                if(input.Length > 1 && input != "spock")
                {
                    input = input[0].ToString();
                }
            }

            return input;
        }

        public void GetAllPlayerValues()
        {
            for(int i = 0; i < PlayerCount; i++ )
            {
                string result = "Error";
                while(result == "Error")
                {
                    result = Interface.GetUserInput("Player "+i);
                    result = CheckUserInput(result);
                }
                Results.Add(result);
            }
        }
        public bool CheckDraw()
        {
            bool allmatch = true;
            foreach(string value in Results)
            {
                if(value != Results[0])
                {
                    allmatch = false;
                }
            }
            return allmatch;
        }
        public Dictionary<string,int> ValueCount()
        {
            Dictionary<string,int> counts = new Dictionary<string,int>();
            string[] valueOptions = new string[]{"r","s","p","spock"};

            foreach(string value in valueOptions)
            {
                int valueCount = Results.Where(option => option == value).Select(option => option).Count();
                counts.Add(value, valueCount);
            }
            return counts;
        }
        public List<int[]> CountWinsAndLosses(Dictionary<string,int> resultCounts)
        {
            List<int[]> gameWinLosses = new List<int[]>();
            foreach(string result in Results)
            {
                int winCount = 0;
                string[] winAgainst = WinRules.Where(value => value.Key == result).FirstOrDefault().Value;
                foreach(string winAgainstValue in winAgainst)
                {
                    winCount += resultCounts.Where(resultPair => resultPair.Key == winAgainstValue).FirstOrDefault().Value;
                }
                int[] playerWinLoss = {winCount, PlayerCount-1-winCount};
                gameWinLosses.Add(playerWinLoss);
            }

            return gameWinLosses;
        }

    }
}