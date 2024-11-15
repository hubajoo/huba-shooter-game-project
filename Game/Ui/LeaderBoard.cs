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

    public LeaderBoard(int width, int height, Player player, string name) : base(width, height)
    {
      _player = player;
      IsVisible = true;
      IsFocused = false;

      _name = name;
      PrintStats();
      DrawBorder();
    }

    public void PrintStats()
    {
      this.Clear();

      this.Print(2, 1, "dead", Color.Black, Color.White);
      //this.Print(2, 1, " Mission: Survive ", Color.Black, Color.White);
      this.DrawBox(new Rectangle(0, 0, Width, Height), ShapeParameters.CreateBorder(new ColoredGlyph(Color.DarkOrange, Color.Black)));
      this.Print(2, 3, " These are your stats:", Color.Black, Color.White);

      this.Print(2, 4, $" Health: {_player.BaseHealth}", Color.White);
      this.Print(2, 5, $" Damage: {_player.BaseDamage}", Color.White);
      this.Print(2, 6, $" Shield: {_player.BaseShield}", Color.White);

      this.Print(2, 8, " Inventory: ", Color.Black, Color.White);
      this.Print(2, 9, $" Number of swords: 0", Color.DarkOrange);

    }
    private void DrawBorder()
    {
      this.DrawBox(new Rectangle(0, 0, Width, Height),
          ShapeParameters.CreateBorder(new ColoredGlyph(Color.Cyan, Color.Black, 176)));
    }

    public override void Update(TimeSpan timeElapsed)
    {
      base.Update(timeElapsed);
      DrawBorder();
    }
  }
}