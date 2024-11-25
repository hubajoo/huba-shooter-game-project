
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects.Items;

/// <summary>
/// The class <c>Potion</c> models a healing item that increases the player's health.
/// </summary>
public class Potion : Item
{
  /// <summary>
  /// The amount of health the potion heals.
  /// </summary>
  public int Healing { get; private set; }

  /// <summary>
  /// Initializes a new instance of <c>Potion</c> with a position, screen object manager, map, and healing amount.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="healing"></param>
  public Potion(Point position, IScreenObjectManager screenObjectManager, IMap map, int healing = 25)
      : base(new ColoredGlyph(Color.Red, Color.Transparent, 3), position, screenObjectManager, map)
  {
    Healing = healing;
  }

  /// <summary>
  /// Increases the player's health by 25.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(Player source)
  {
    source.Health += 25;
    RemoveSelf();
    return true;
  }
}