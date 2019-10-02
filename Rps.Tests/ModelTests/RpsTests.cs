using System;
using System.Collections.Generic;
using Rps.Models;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rps.Tests
{
  [TestClass]
  public class RpsTest
  {
    Game game;

    [TestInitialize]
    public void Startup()
    {
        game = new Game(1,2);
        game.Results.Add("s");
        game.Results.Add("r");
    }

    [TestCleanup]
    public void testClean()
    {
        game = null;
    }

    [TestMethod]
    public void CheckUserInput_ReturnConvertedString_String()
    {
        string p1Value = game.CheckUserInput("Scissors");
        Assert.AreEqual("s", p1Value);
    }
    [TestMethod]
    public void CheckUserInput_ReturnErrorIfNoTValid_String()
    {
        string p1Value = game.CheckUserInput("sock");
        Assert.AreEqual("Error", p1Value);  
    }

    [TestMethod]
    public void ResultsProp_StoreValidUserValues_ListOfStrings()
    {
        List<string> expected = new List<string>(){"s", "r"};
        Assert.AreEqual(true,expected.SequenceEqual(game.Results));  
    }
    [TestMethod]
    public void CheckDraw_CheckForDrawCondition_True()
    {
        game = new Game(1,2);
        game.Results.Add("s");
        game.Results.Add("s");
        bool draw = game.CheckDraw();
        Assert.AreEqual(true, draw);
    }

    [TestMethod]
    public void ValueCount_CountResultValues_Dictionary()
    {   
        Dictionary<string,int> counts = game.ValueCount();
        int rCount = counts.Where(value => value.Key == "r").FirstOrDefault().Value;
        int sCount = counts.Where(value => value.Key == "s").FirstOrDefault().Value;
        int pCount = counts.Where(value => value.Key == "p").FirstOrDefault().Value;

        Assert.AreEqual(1, rCount);
        Assert.AreEqual(1, sCount);
        Assert.AreEqual(0, pCount);
    }

    [TestMethod]
    public void CountWinsLosses_CountWinsAndLosses_IntArray()
    {   
        Dictionary<string,int> counts = game.ValueCount();
        int P1 = -1;
        int P2 = 1;
        List<int> expected = new List<int>(){P1,P2};

        Assert.AreEqual(true,expected.SequenceEqual(game.CountWinsAndLosses(counts)));
    }

    [TestMethod]
    public void DeclareWinner_DecidesWinner_String()
    {
        Dictionary<string,int> countResultTypes = game.ValueCount();
        List<int> winLoseList = game.CountWinsAndLosses(countResultTypes);
        string winner = game.DeclareWinner(winLoseList);

        Assert.AreEqual("Player 2", winner);
    }
    [TestMethod]
    public void OverAllWinner_CheckOverAllWinCondition_String()
    {
        Dictionary<string,int> countResultTypes = game.ValueCount();
        List<int> winLoseList = game.CountWinsAndLosses(countResultTypes);
        string winner = game.DeclareWinner(winLoseList);
        List<string> winners = game.OverAllWinner();
        List<string> expected = new List<string> (){"Player 2"};
        CollectionAssert.AreEquivalent(expected,winners);
    }
  }
}
