using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects.Items;

/// <summary>
/// The class <c>RangeBonus</c> models an item that increases the player's range.
/// </summary>
public class RangeBonus : Item
{
  /// <summary>
  /// Initializes a new instance of <c>RangeBonus</c> with a position, screen object manager, and map.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  public RangeBonus(Point position, IScreenObjectManager screenObjectManager, Map map)
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