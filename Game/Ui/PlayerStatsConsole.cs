using System;
using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace DungeonCrawl.UI
{
  public class PlayerStatsConsole : Console
  {
    private Player _player;
    private string _name;

    private int _borderOffset;
    private int fullWidth;
    private int fullHeight;
    public PlayerStatsConsole(int width, int height, Player player, string name, int borderOffset = 1) : base(width, height)
    {
      _player = player;
      IsVisible = true;
      IsFocused = false;
      _borderOffset = borderOffset;
      fullWidth = width;
      fullHeight = height;

      _name = name;
      //PrintStats();
      //DrawBorder();
      string[] a = new string[] { "Huba: 1000", "Bela: 500", "Gyuri: 200" };
      //PrintLeaderBoard(a);
    }

    public void PrintStats()
    {
      this.Clear();
      int leftMargin = _borderOffset + 1;

      this.Print(leftMargin, 1, " Mission: Survive ", Color.Black, Color.White);
      this.Print(leftMargin, 2, $" User: {_name} ", Color.White);

      string mission = "Kill the monsters coming from the portals and survive as long as you can.";
      //string mission = "1 2 3 4 5 6 7 8 9 9 0 1 3 4 5 6 7 8 9";
      string[] missionArr = mission.Split(" ");

      int line = 3 + _borderOffset;
      string lineText = "";
      for (int i = 0; i < missionArr.Length; i++)
      {
        
        if (lineText.Length + missionArr[i].Length > 0)
        {
          this.Print(leftMargin, line, lineText, Color.DarkOrange);
          lineText = "";
          //i--;
          line++;
        }
        else
        {
          lineText += " " + missionArr[i];
        }
        
      }
      /*
            this.Print(2, 5, " Player stats:", Color.Black, Color.White);

            this.Print(leftMargin, 7, $" Health: {_player.Health}", Color.White);
            this.Print(leftMargin, 8, $" Damage: {_player.Damage}", Color.White);
            this.Print(leftMargin, 9, $" Range: {_player.Range}", Color.White);
            this.Print(leftMargin, 10, $" Score: {_player.Kills}", Color.White);
            */
    }
    public void PrintLeaderBoard(string[] leaderBoard)
    {
      for (int i = 0; i < leaderBoard.Length; i++)
      {
        this.Print(2, 11 + i, $"{i + 1}. {leaderBoard[i]}", Color.White);
      }
    }
    public void DrawBorder()
    {
      this.DrawBox(new Rectangle(0, 0, fullWidth, fullHeight),
          ShapeParameters.CreateBorder(new ColoredGlyph(Color.White, Color.Black, 176)));
    }

    public override void Update(TimeSpan timeElapsed)
    {
      PrintStats();
      //PrintLeaderBoard();
      DrawBorder();
    }
  }
}