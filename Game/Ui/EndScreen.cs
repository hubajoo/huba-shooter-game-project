using System;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace DungeonCrawl.UI
{
  /// <summary>
  /// Class <c>EndScreen</c> models the end screen of the game.
  /// </summary>
  public class EndScreen : Console
  {
    private int _center;
    private ILeaderBoardHandler _leaderBoardHandler;
    private int _borderOffset;
    public int LeaderBoardOffset { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="leaderBoardHandler"></param>
    /// <param name="borderOffset"></param>
    public EndScreen(IGameSettings settings, ILeaderBoardHandler leaderBoardHandler, int borderOffset = 10) : base(settings.ViewPortWidth, settings.ViewPortHeight)
    {
      IsVisible = true;
      IsFocused = false;

      _center = Width / 2;
      _leaderBoardHandler = leaderBoardHandler;

      LeaderBoardOffset = 10;
      _borderOffset = borderOffset;

      PrintLeaderBoard(_leaderBoardHandler.GetArray());
    }
    /// <summary>
    /// Prints death message and prompts player to quit the game.
    /// </summary>
    public void DeathMessage()
    {
      PrintCenter(" You died ", 2, Color.Red, Color.White);
      PrintCenter(" Press Space to quit ", 3, Color.DarkOrange);
    }
    /// <summary>
    /// Prints text in the horizontal center.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="y"></param>
    /// <param name="foreground"></param>
    /// <param name="background"></param>
    private void PrintCenter(string text, int y, Color foreground, Color background)
    {
      this.Print(_center - (text.Length / 2), y, text, foreground, background);
    }
    /// <summary>
    /// Prints text in the horizontal center.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="y"></param>
    /// <param name="foreground"></param>
    private void PrintCenter(string text, int y, Color foreground)
    {
      this.Print(_center - (text.Length / 2), y, text, foreground);
    }
    /// <summary>
    /// Draws a box.
    /// </summary>
    private void DrawBorder()
    {
      this.DrawBox(new Rectangle(0, 0, Width, Height),
          ShapeParameters.CreateBorder(new ColoredGlyph(Color.Red, Color.Black, 176)));
    }
    /// <summary>
    /// Prints the leaderboard.
    /// </summary>
    /// <param name="leaderBoard"></param>
    /// <param name="startNum"></param>
    /// <param name="leftOffset"></param>
    public void PrintLeaderBoard(string[] leaderBoard, int startNum = 0, int leftOffset = 0)
    {
      int line = LeaderBoardOffset + 1;
      for (int i = startNum; i < leaderBoard.Length; i++)
      {
        if (line < Height - _borderOffset * 2)
        {
          PrintCenter($"{i + 1}. {leaderBoard[i]}", line, Color.White);
          line++;
        }
      }

    }
    /// <summary>
    /// Processes the keyboard input.
    /// </summary>
    /// <param name="keyboard"></param>
    /// <returns></returns>
    public override bool ProcessKeyboard(Keyboard keyboard)
    {
      if (keyboard.IsKeyPressed(Keys.Space))
      {
        Game.Instance.Dispose();
      }
      return true;
    }
    /// <summary>
    /// Updates the screen.
    /// </summary>
    /// <param name="delta"></param>
    public override void Update(TimeSpan delta)
    {
      DeathMessage();
      DrawBorder();
      PrintLeaderBoard(_leaderBoardHandler.GetArray(), 0, 2);
      Game.Instance.Screen.IsFocused = true;
    }
  }
}