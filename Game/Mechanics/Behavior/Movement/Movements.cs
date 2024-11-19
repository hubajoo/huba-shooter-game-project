namespace DungeonCrawl.Mechanics;

using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;
/// <summary>
/// Class <c>Movement</c> models the movement logic for a game object.
/// </summary>
public class Movements : IMovementLogic
{

  private IMap _map;

  private IScreenObjectManager _screenObjectManager;

  /// <summary> Constructor. </summary>
  /// <param name="map"></param>
  /// <param name="screenObjectManager"></param>
  public Movements(IMap map, IScreenObjectManager screenObjectManager)
  {
    _map = map;
    _screenObjectManager = screenObjectManager;
  }

  /// <summary>
  /// Moves a game object to a new position on the map.
  /// </summary>
  /// <param name="gameObject"></param>
  /// <param name="map"></param>
  /// <param name="newPosition"></param>
  /// <returns> Bool </returns>
  public bool Move(IGameObject gameObject, IMap map, Point newPosition)
  {
    // Check new position is valid
    if (!map.SurfaceObject.Surface.IsValidCell(newPosition.X, newPosition.Y)) return false;

    // Check if other object is there
    if (map.TryGetMapObject(newPosition, out IGameObject foundObject))
    {
      // We touched the other object, but they won't allow us to move into the space
      if (!foundObject.Touched(gameObject))
      {
        return false;
      }
    }

    // Save the old position
    Point oldPosition = gameObject.Position;

    // Update the game object's position
    gameObject.Position = newPosition;

    // Update cells 
    _screenObjectManager.RefreshCell(map, newPosition);
    _screenObjectManager.RefreshCell(map, oldPosition);

    return true;
  }

}