using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

public interface IDirectionChoiche
{
  public Direction GetDirection(Point position, Point targetPosition);
  public Direction GetDirection();
}