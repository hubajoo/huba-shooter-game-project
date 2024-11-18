using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;

/// <summary>
/// The interface <I>IGameObject</I> models any objects in the game.
/// </summary>
public interface IGameObject
{
  /// <summary> Calls contact logic for this game object. </summary>
  public bool Touched(IGameObject source);
  /// <summary>
  /// Calls contact logic for the touchhed game object.
  /// </summary>
  /// <param name="source"></param>
  public void Touching(IGameObject source);
  /// <summary>
  /// Updates the game object.
  /// </summary>
  public void Update();
  /// <summary>
  /// Removes the game object from the map.
  /// </summary>
  public void RemoveSelf();
  /// <summary>
  /// Gets the position of the game object.
  /// </summary>
  /// <returns>Point</returns>
  public Point GetPosition();
  /// <summary>
  /// Gets the appearance of the game object. 
  /// </summary>
  public ColoredGlyph GetAppearance();
}