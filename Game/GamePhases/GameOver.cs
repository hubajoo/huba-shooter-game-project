using System;
using DungeonCrawl.Ui;
using DungeonCrawl.UI;


public class GameOver
{

  private RootScreen rootScreen;
  private LeaderBoard leaderBoard;

  public GameOver(RootScreen _rootScreen, LeaderBoard _leaderBoard)
  {
    rootScreen = _rootScreen;
    leaderBoard = _leaderBoard;
  }

  public void End()
  {
    //_sc.ClearScreen();
    //rootScreen.AddScreenObject(leaderBoard);
  }

}