using System;
using System.Collections.Generic;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

/// <summary> 
/// GEnerates directions for movement or projectiles.
/// </summary>
public class DirectionGeneration
{
  /// <summary> Generates a random direction. </summary>
  public static Direction GetRandomDirection()
  {
    Random rnd = new Random();
    int randomDirectionNumber = rnd.Next(4);
    Direction[] directions = new[] { Direction.Down, Direction.Up, Direction.Left, Direction.Right, };
    Direction direction = directions[randomDirectionNumber];
    return direction;
  }
  /// <summary>
  /// Generates a direction that moves towards the target.
  /// </summary>
  /// <param name="hunterPosition"></param>
  /// <param name="targetPosition"></param>
  /// <returns></returns>
  public static Direction AggressiveDirection(Point hunterPosition, Point targetPosition)
  { //Checks the targets position and returns movements that approach the target
    Random rnd = new Random();

    List<Direction> possibleDirections = new List<Direction>();

    if (targetPosition.X > hunterPosition.X)
    {
      possibleDirections.Add(Direction.Right);
    }
    if (targetPosition.X < hunterPosition.X)
    {
      possibleDirections.Add(Direction.Left);
    }
    if (targetPosition.Y < hunterPosition.Y)
    {
      possibleDirections.Add(Direction.Up);
    }
    if (targetPosition.Y > hunterPosition.Y)
    {
      possibleDirections.Add(Direction.Down);
    }

    int randomDirectionNumber = rnd.Next(possibleDirections.Count);
    if (possibleDirections.Count == 0) //Error handling
    {
      return GetRandomDirection();
    }


    return possibleDirections[randomDirectionNumber];
  }

}