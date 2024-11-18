using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;
/// <summary>
/// Interface <c>IMovementLogic</c> defines the movement logic for a game object.
/// </summary>
public interface IMovementLogic
{
  /// <summary>
  /// Moves a game object to a new position on the map.
  /// </summary>
  /// <param name="gameObject"></param>
  /// <param name="map"></param>
  /// <param name="newPosition"></param>
  /// <returns></returns>
  public bool Move(GameObject gameObject, Map map, Point newPosition);
}