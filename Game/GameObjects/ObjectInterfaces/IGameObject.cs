using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects.ObjectInterfaces;

/// <summary>
/// The interface <I>IGameObject</I> models any objects in the game.
/// </summary>
public interface IGameObject
{
  /// <summary>
  /// The appearance of the game object.
  /// </summary>
  public ColoredGlyph Appearance { get; set; }

  /// <summary>
  /// The position of the game object.
  /// </summary>
  public Point Position { get; set; }

  /// <summary>
  /// Checks if the game object has been touched.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
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