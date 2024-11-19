using System;
using SadRogue.Primitives;
namespace DungeonCrawl.Mechanics;

public class RandomDirection : IDirectionChoiche
{
  public Direction GetDirection()
  {
    Random rnd = new Random();
    int randomDirectionNumber = rnd.Next(4);
    Direction[] directions = new[] { Direction.Down, Direction.Up, Direction.Left, Direction.Right, };
    Direction direction = directions[randomDirectionNumber];
    return direction;
  }
  public Direction GetDirection(Point position, Point targetPosition)
  {
    return GetDirection();
  }
}