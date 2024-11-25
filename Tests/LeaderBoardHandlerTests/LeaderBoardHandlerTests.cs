using NUnit.Framework;
using Moq;
using Moq.Protected; // Include this namespace
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DungeonCrawl.LeaderBoard;
using DungeonCrawl.GameSetup;

namespace Tests.LeaderBoardHandlerTests
{
  public class LeaderBoardHandlerTests
  {
    private HttpClient _httpClient;
    private string _url = "http://example.com";
    private string _validPath = "Data/leaderboard_valid.txt";
    private string _emptyFilePath = "Data/leaderboard_empty.txt";
    private string _invalidPath = "Data/leaderboard_invalid.txt";
    private string _addEntryPath = "Data/leaderboard_add_entry.txt";


    [SetUp]
    public void Setup()
    {

      //Create and empty file at all paths
      File.WriteAllText(_validPath, "");
      File.WriteAllText(_emptyFilePath, "");
      File.WriteAllText(_invalidPath, "");
      File.WriteAllText(_addEntryPath, "");
    }


    [Test]
    public async Task ReadLeaderBoard_Success()
    {
      var gameSettings = new Mock<IGameSettings>();
      gameSettings.Setup(gs => gs.ServerUrl).Returns(_url);

      ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(gameSettings.Object, _validPath);

      File.WriteAllText(_validPath, "Player1:100");
      leaderBoardHandler.ReadLeaderBoard();
      var leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(1));
      Assert.That(leaderboard[0], Is.EqualTo("Player1:100"));

      File.WriteAllText(_validPath, "Player2:200");
      leaderBoardHandler.ReadLeaderBoard();
      leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(1));
      Assert.That(leaderboard[0], Is.EqualTo("Player2:200"));

    }

    [Test]
    public async Task ReadLeaderBoard_InvalidPath()
    {
      var gameSettings = new Mock<IGameSettings>();
      gameSettings.Setup(gs => gs.ServerUrl).Returns(_url);

      ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(gameSettings.Object, _invalidPath);
      leaderBoardHandler.ReadLeaderBoard();
      var leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task ReadLeaderBoard_EmptyFile()
    {
      var gameSettings = new Mock<IGameSettings>();
      gameSettings.Setup(gs => gs.ServerUrl).Returns(_url);

      ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(gameSettings.Object, _emptyFilePath);
      leaderBoardHandler.ReadLeaderBoard();
      var leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(0));
    }

    [Test]
    public void AddToLeaderboard_Success()
    {
      var gameSettings = new Mock<IGameSettings>();
      gameSettings.Setup(gs => gs.ServerUrl).Returns(_url);

      ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(gameSettings.Object, _addEntryPath);

      leaderBoardHandler.AddToLeaderboard("Player1", 100);
      var leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(1));
      Assert.That(leaderboard[0], Is.EqualTo("Player1:100"));


      leaderBoardHandler.AddToLeaderboard("Player2", 200);
      leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(2));
      Assert.That(leaderboard[1], Is.EqualTo("Player1:100"));
      Assert.That(leaderboard[0], Is.EqualTo("Player2:200"));
    }

    [Test]
    public void SortLeaderBoard_Success()
    {
      var gameSettings = new Mock<IGameSettings>();
      gameSettings.Setup(gs => gs.ServerUrl).Returns(_url);

      ILeaderBoardHandler leaderBoardHandler = new LeaderBoardHandler(gameSettings.Object, _addEntryPath);

      leaderBoardHandler.AddToLeaderboard("Player1", 100);
      leaderBoardHandler.AddToLeaderboard("Player2", 200);
      leaderBoardHandler.AddToLeaderboard("Player3", 50);
      leaderBoardHandler.AddToLeaderboard("Player4", 300);
      leaderBoardHandler.AddToLeaderboard("Player5", 150);

      var leaderboard = leaderBoardHandler.GetArray();
      Assert.That(leaderboard.Count(), Is.EqualTo(5));
      Assert.That(leaderboard[0], Is.EqualTo("Player4:300"));
      Assert.That(leaderboard[1], Is.EqualTo("Player2:200"));
      Assert.That(leaderboard[2], Is.EqualTo("Player5:150"));
      Assert.That(leaderboard[3], Is.EqualTo("Player1:100"));
      Assert.That(leaderboard[4], Is.EqualTo("Player3:50"));
    }
  }
}