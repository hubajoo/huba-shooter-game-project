using DungeonCrawl.Maps;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;
/// <summary>
/// Interface <c>IMoving</c> defines the movement logic for a game object.
/// </summary>
public interface IMoving
{
  /// <summary>
  /// Moves the object to the given position on the map.
  /// </summary>
  /// <param name="newPosition"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public bool Move(Point newPosition, IMap map);
}
