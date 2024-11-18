
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;

/// <summary>
/// The class <c>Potion</c> models a healing item in the game.
/// </summary>
public class Potion : Items
{
  /// <summary>
  /// Gets the healing value of the potion.
  public int Healing { get; private set; }
  /// <summary>
  /// Initializes a new instance of <c>Potion</c> with a position, screen object manager, map, and healing value.
  public Potion(Point position, ScreenObjectManager screenObjectManager, Map map, int healing = 25)
      : base(new ColoredGlyph(Color.Red, Color.Transparent, 3), position, screenObjectManager, map)
  {
    Healing = healing;
  }
  /// <summary>
  public override bool Touched(Player source)
  {
    source.BaseHealth += 25;
    RemoveSelf();
    return true;
  }
}