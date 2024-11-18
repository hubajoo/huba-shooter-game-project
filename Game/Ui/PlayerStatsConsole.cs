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

    public PlayerStatsConsole(int width, int height, Player player, string name) : base(width, height)
    {
      _player = player;
      IsVisible = true;
      IsFocused = false;

      _name = name;
      //PrintStats();
      DrawBorder();
    }

    public void PrintStats()
    {
      this.Clear();

      this.Print(2, 2, $" User: {_name}", Color.White);
      this.DrawBox(new Rectangle(0, 0, Width, Height), ShapeParameters.CreateBorder(new ColoredGlyph(Color.DarkOrange, Color.Black)));
      this.DrawBox(new Rectangle(3, 10, 20, 20), ShapeParameters.CreateBorder(new ColoredGlyph(Color.DarkOrange, Color.Black)));
      this.Print(2, 4, " Player stats:", Color.Black, Color.White);

      this.Print(2, 5, $" Health: {_player.Health}", Color.White);
      this.Print(2, 6, $" Damage: {_player.Damage}", Color.White);
      this.Print(2, 7, $" Range: {_player.Range}", Color.White);
      this.Print(2, 8, $" Score: {_player.Kills}", Color.White);

      this.Print(2, 9, $" Top Players", Color.Black, Color.White);
      this.Print(2, 10, $" Number of Kills: {_player.Kills}", Color.DarkOrange);
    }
    private void DrawBorder()
    {
      this.DrawBox(new Rectangle(0, 0, Width, Height),
          ShapeParameters.CreateBorder(new ColoredGlyph(Color.White, Color.Black, 176)));
    }

    public override void Update(TimeSpan timeElapsed)
    {
     PrintStats();
      //base.Update(timeElapsed);
      ///DrawBorder();
    }
  }
}