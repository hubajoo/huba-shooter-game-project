using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

public interface IMovementLogic
{
  public bool Move(GameObject gameObject, Map map, Point newPosition);
}