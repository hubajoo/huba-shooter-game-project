using DungeonCrawl.Maps;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;
public interface IMoving
{
  /// <summary>
  /// Moves the object to the given position on the map.
  /// </summary>
  /// <param name="newPosition"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public bool Move(Point newPosition, Map map);
}
