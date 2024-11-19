using System;
using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace DungeonCrawl.UI
{
  public class LeaderBoard : Console
  {
    private Player _player;
    private string _name;

    private int _center;

    public LeaderBoard(int width, int height, Player player, string name) : base(width, height)
    {
      _player = player;
      IsVisible = true;
      IsFocused = false;

      _name = name;
      _center = Width / 2;
      PrintStats();
      DrawBorder();
    }

    public void PrintStats()
    {
      this.Clear();

      this.Print(_center, 2, " You died ", Color.Red, Color.White);
      //this.Print(2, 1, " Mission: Survive ", Color.Black, Color.White);
       this.DrawBox(new Rectangle(0, 0, Width, Height), ShapeParameters.CreateBorder(new ColoredGlyph(Color.DarkOrange, Color.Black)));
      this.Print(2, 3, " Score:", Color.Black, Color.White);

      this.Print(2, 4, $" Kills: {_player.Kills}", Color.White);

      this.Print(2, 8, " High scores: ", Color.Black, Color.White);
      this.Print(2, 9, $" Huba: 1000", Color.White);

    }
    private void DrawBorder()
    {
      this.DrawBox(new Rectangle(0, 0, Width, Height),
          ShapeParameters.CreateBorder(new ColoredGlyph(Color.Red, Color.Black, 176)));
    }

    public override void Update(TimeSpan timeElapsed)
    {
      base.Update(timeElapsed);
      DrawBorder();
    }
  }
}