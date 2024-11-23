using System;
using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace DungeonCrawl.UI;

/// <summary>
/// Class <c>PlayerStatsConsole</c> models a console for player stats.
/// </summary>
public class PlayerStatsConsole : Console
{
  private Player _player;
  private string _name;

  private int _borderOffset;
  private int fullWidth;
  private int fullHeight;
  private int LeaderBoardOffset = 0;
  private int leftMargin;

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="width"></param>
  /// <param name="height"></param>
  /// <param name="player"></param>
  /// <param name="name"></param>
  /// <param name="leaderboard"></param>
  /// <param name="borderOffset"></param>
  public PlayerStatsConsole(int width, int height, Player player, string name, string[] leaderboard, int borderOffset = 1) : base(width, height)
  {
    _player = player;
    IsVisible = true;
    IsFocused = false;
    _borderOffset = borderOffset;
    fullWidth = width;
    fullHeight = height;
    leftMargin = _borderOffset + 1;
    _name = name;

    PrintStats();
    DrawBorder();
    PrintLeaderBoard(leaderboard);
  }

  /// <summary>
  /// Prints player stats.
  /// </summary>
  public void PrintStats()
  {
    var area = new Rectangle(_borderOffset, _borderOffset, Width - _borderOffset * 2, LeaderBoardOffset);
    this.Clear(area);

    int line = _borderOffset + 1;

    this.Print(leftMargin, line, " Mission: Survive ", Color.Black, Color.White);
    line++;
    this.Print(leftMargin, line + 1, $" User: {_name} ", Color.White);
    line += 3;

    string mission = "Kill the monsters coming from the portals and survive as long as you can.";
    string[] missionArr = mission.Split(" ");

    string lineText = "";
    for (int i = 0; i < missionArr.Length; i++)
    {
      if (lineText.Length + missionArr[i].Length > 20 || i == missionArr.Length - 1)
      {
        lineText += " " + missionArr[i];
        this.Print(leftMargin, line, lineText, Color.DarkOrange);
        lineText = "";
        line++;
      }
      else
      {
        lineText += " " + missionArr[i];
      }
    }
    line++;
    this.Print(leftMargin, line, " Player stats:", Color.Black, Color.White);
    line++;
    this.Print(leftMargin, line, $" Health: {_player.Health}", Color.White);
    line++;
    this.Print(leftMargin, line, $" Damage: {_player.Damage}", Color.White);
    line++;
    this.Print(leftMargin, line, $" Range: {_player.Range}", Color.White);
    line++;
    this.Print(leftMargin, line, $" Score: {_player.Kills}", Color.White);
    line++;
    LeaderBoardOffset = line;
  }

  /// <summary>
  /// Prints the leaderboard.
  /// </summary>
  /// <param name="leaderBoard"></param>
  public void PrintLeaderBoard(string[] leaderBoard)
  {
    int line = LeaderBoardOffset + 1;
    this.Print(leftMargin, line, $"Top players:", Color.Black, Color.White);
    line++;
    for (int i = 0; i < leaderBoard.Length; i++)
    {
      if (line < Height - _borderOffset * 2)
      {
        this.Print(leftMargin, line, $"{i + 1}. {leaderBoard[i]}", Color.White);
        line++;
      }
    }
  }

  /// <summary>
  /// Draws a box.
  /// </summary>
  public void DrawBorder()
  {
    this.DrawBox(new Rectangle(0, 0, fullWidth, fullHeight),
        ShapeParameters.CreateBorder(new ColoredGlyph(Color.White, Color.Black, 176)));
  }

  /// <summary>
  /// Updates the console.
  /// </summary>
  /// <param name="timeElapsed"></param>
  public override void Update(TimeSpan timeElapsed)
  {
    PrintStats();
  }
}
