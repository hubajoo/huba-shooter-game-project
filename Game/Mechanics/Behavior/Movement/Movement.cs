namespace DungeonCrawl.Mechanics;

using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;
/// <summary>
/// Class <c>Movement</c> models the movement logic for a game object.
/// </summary>
public class Movement : IMovementLogic
{

  private Map _map;

  private ScreenObjectManager _screenObjectManager;

  /// <summary> Constructor. </summary>
  /// <param name="map"></param>
  /// <param name="screenObjectManager"></param>
  public Movement(Map map, ScreenObjectManager screenObjectManager)
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
  public bool Move(GameObject gameObject, Map map, Point newPosition)
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