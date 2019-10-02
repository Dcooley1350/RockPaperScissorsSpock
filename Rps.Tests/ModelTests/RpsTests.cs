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
    [TestMethod]
    public void CheckUserInput_ReturnConvertedString_String()
    {
        Game game = new Game(1,2);
        string p1Value = game.CheckUserInput("Scissors");
        Assert.AreEqual("s", p1Value);
    }
    [TestMethod]
    public void CheckUserInput_ReturnErrorIfNoTValid_String()
    {
        Game game = new Game(1,2);
        string p1Value = game.CheckUserInput("sock");
        Assert.AreEqual("Error", p1Value);  
    }

    [TestMethod]
    public void ResultsProp_StoreValidUserValues_ListOfStrings()
    {
        Game game = new Game(1,2);
        game.Results.Add("s");
        game.Results.Add("r");
        List<string> expected = new List<string>(){"s", "r"};
        Assert.AreEqual(true,expected.SequenceEqual(game.Results));  
    }
    [TestMethod]
    public void CheckDraw_CheckForDrawCondition_True()
    {
        Game game = new Game(1,2);
        game.Results.Add("s");
        game.Results.Add("s");
        bool draw = game.CheckDraw();
        Assert.AreEqual(true, draw);
    }

    [TestMethod]
    public void ValueCount_CountResultValues_Dictionary()
    {   
        Game game = new Game(1,2);
        game.Results.Add("s");
        game.Results.Add("r");
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
        Game game = new Game(1,2);
        game.Results.Add("s");
        game.Results.Add("r");
        Dictionary<string,int> counts = game.ValueCount();
        int[] P1 = {0,1};
        int[] P2 = {1,0};
        List<int[]> expected = new List<int[]>(){P1,P2};

        Assert.AreEqual(true,expected[0].SequenceEqual(game.CountWinsAndLosses(counts)[0]));
        Assert.AreEqual(true,expected[1].SequenceEqual(game.CountWinsAndLosses(counts)[1]));
    }
  }
}
