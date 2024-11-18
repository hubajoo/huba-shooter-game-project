using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;

/// <summary>
/// The class <c>RangeBonus</c> models a range bonus item in the game.
/// </summary>
public class RangeBonus : Items
{
  /// <summary>
  /// Initializes a new instance of <c>RangeBonus</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  public RangeBonus(Point position, ScreenObjectManager screenObjectManager, Map map)
      : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 'R'), position, screenObjectManager, map)
  {
  }
  /// <summary>
  /// Increases the player's range by 3.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(Player source)
  {
    source.Range += 3;
    RemoveSelf();
    return true;
  }
}