using DungeonCrawl.Maps;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;
public interface IMoving
{
  public bool Move(Point newPosition, Map map);
}
