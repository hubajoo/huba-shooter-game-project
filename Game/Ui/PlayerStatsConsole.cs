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
  private int _fullWidth;
  private int _fullHeight;
  private int _leaderBoardOffset;
  private int _leftMargin;

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
    _fullWidth = width;
    _fullHeight = height;
    _leftMargin = _borderOffset + 1;
    _name = name;
    _leaderBoardOffset = 0;

    PrintStats();
    DrawBorder();
    PrintLeaderBoard(leaderboard);
  }

  /// <summary>
  /// Prints player stats.
  /// </summary>
  public void PrintStats()
  {
    var area = new Rectangle(_borderOffset, _borderOffset, Width - _borderOffset * 2, _leaderBoardOffset);
    this.Clear(area);

    int line = _borderOffset + 1;

    this.Print(_leftMargin, line, " Mission: Survive ", Color.Black, Color.White);
    line++;
    this.Print(_leftMargin, line + 1, $" User: {_name} ", Color.White);
    line += 3;

    string mission = "Kill the monsters coming from the portals and survive as long as you can.";
    string[] missionArr = mission.Split(" ");

    string lineText = "";
    for (int i = 0; i < missionArr.Length; i++)
    {
      if (lineText.Length + missionArr[i].Length > 20 || i == missionArr.Length - 1)
      {
        lineText += " " + missionArr[i];
        this.Print(_leftMargin, line, lineText, Color.DarkOrange);
        lineText = "";
        line++;
      }
      else
      {
        lineText += " " + missionArr[i];
      }
    }
    line++;
    this.Print(_leftMargin, line, " Player stats:", Color.Black, Color.White);
    line++;
    this.Print(_leftMargin, line, $" Health: {_player.Health}", Color.White);
    line++;
    this.Print(_leftMargin, line, $" Damage: {_player.Damage}", Color.White);
    line++;
    this.Print(_leftMargin, line, $" Range: {_player.Range}", Color.White);
    line++;
    this.Print(_leftMargin, line, $" Score: {_player.Kills}", Color.White);
    line++;
    _leaderBoardOffset = line;
  }

  /// <summary>
  /// Prints the leaderboard.
  /// </summary>
  /// <param name="leaderBoard"></param>
  public void PrintLeaderBoard(string[] leaderBoard)
  {
    int line = _leaderBoardOffset + 1;
    this.Print(_leftMargin, line, $"Top players:", Color.Black, Color.White);
    line++;
    for (int i = 0; i < leaderBoard.Length; i++)
    {
      if (line < Height - _borderOffset * 2)
      {
        this.Print(_leftMargin, line, $"{i + 1}. {leaderBoard[i]}", Color.White);
        line++;
      }
    }
  }

  /// <summary>
  /// Draws a box.
  /// </summary>
  public void DrawBorder()
  {
    this.DrawBox(new Rectangle(0, 0, _fullWidth, _fullHeight),
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
