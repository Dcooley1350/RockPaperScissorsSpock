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
        public List<int> WINES {get;}
        public int Rounds {get;}
        public int PlayerCount {get;}
        public Dictionary<string,string[]> WinRules {get;}
        public Dictionary<string,string[]> LossRules {get;}
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
            LossRules = new Dictionary<string, string[]>();
            LossRules.Add("r", new string[]{"p"});
            LossRules.Add("p", new string[]{"s"});
            LossRules.Add("s", new string[]{"r"});
            LossRules.Add("spock", new string[]{""});
            WINES = new List<int>();
            for(int i = 0; i< playerCount; i++)
            {
                WINES.Add(0);
            }
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
                    result = Interface.GetUserInput("Player "+(i+1));
                    result = CheckUserInput(result);
                }
                Results.Add(result);
            }
        }

        public void PlayRound()
        {
            GetAllPlayerValues();
            Dictionary<string,int> countResultTypes = ValueCount();
            List<int> winLoseList = CountWinsAndLosses(countResultTypes);
            string winner = DeclareWinner(winLoseList);
            Console.WriteLine("Round Winner(s): " + winner + "!");
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

        public int CountWins(Dictionary<string,int> resultCounts, string playerResult)
        {
            int winCount = 0;
            string[] winAgainst = WinRules.Where(value => value.Key == playerResult).FirstOrDefault().Value;
            foreach(string winAgainstValue in winAgainst)
            {
                winCount += resultCounts.Where(resultPair => resultPair.Key == winAgainstValue).FirstOrDefault().Value;
            }
            return winCount;
        }

        public int CountLosses(Dictionary<string,int> resultCounts, string playerResult)
        {
            int lossCount = 0;
            string[] loseAgainst = LossRules.Where(value => value.Key == playerResult).FirstOrDefault().Value;
            foreach(string loseAgainstValue in loseAgainst)
            {
                lossCount += resultCounts.Where(resultPair => resultPair.Key == loseAgainstValue).FirstOrDefault().Value;
            }
            return lossCount;
        }


        public List<int> CountWinsAndLosses(Dictionary<string,int> resultCounts)
        {
            List<int> gameWinLosses = new List<int>();
            foreach(string result in Results)
            {
                int winCount = CountWins(resultCounts, result);
                int loseCount = CountLosses(resultCounts, result);
                int playerWinLoss = winCount - loseCount;
                gameWinLosses.Add(playerWinLoss);
            }

            return gameWinLosses;
        }

        public string DeclareWinner(List<int> intList)
        {
             int winNum = intList.Max();
             intList.IndexOf(winNum);
             string winner = "";
             List<int> winnerIndicies = Enumerable.Range(0,intList.Count).Where(index => intList[index] == winNum).ToList();
             if(winnerIndicies.Count == 1)
             {
                winner = "Player " + (winnerIndicies[0]+1).ToString();
                WINES[winnerIndicies[0]]++;
             }
             else if(winnerIndicies.Count == PlayerCount)
             {
                winner = "None";
             }
             else
             {
                winner = "Draw between : ";
                foreach(int index in winnerIndicies)
                {
                    winner += "Player " + (winnerIndicies[index]+1) + " ";
                    WINES[winnerIndicies[index]]++;
                }
             }
             return winner;

        }
        public List<string> OverAllWinner()
        {
            List<string> gameWinners = new List<string>();
            for(int i = 0; i<WINES.Count; i++)
            {
                if(WINES[i] == (Rounds/2+1))
                {
                    gameWinners.Add("Player " + (i+1).ToString());
                }
            } 
            return gameWinners;               
        }
    }
}