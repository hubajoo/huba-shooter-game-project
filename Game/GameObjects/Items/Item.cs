using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects.Items;

/// <summary>
/// The class <c>Item</c> models an item in the game.
/// </summary>
public abstract class Item : GameObject
{
  protected Item(ColoredGlyph appearance, Point position, IScreenObjectManager screenObjectManager, Map map) : base(appearance, position, screenObjectManager, map)
  {

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public virtual bool Touched(Player source)
  {
    return true;
  }

  /// <summary>
  ///  Method <c>Touched</c> is called when the item is touched by a player.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(IGameObject source)
  {
    if (source is Player player) // If the source is a player, call the Touched method with the player.
    {
      RemoveSelf(); // Remove the item from the map.
      return Touched(player); // Call the Touched method with the player.
    }
    return true;
  }
}
