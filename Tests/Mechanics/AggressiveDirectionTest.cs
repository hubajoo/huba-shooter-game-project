using DungeonCrawl.Mechanics;
using NUnit.Framework;
using SadRogue.Primitives;
using System.Collections.Generic;

namespace Test.Mechanics
{
  [TestFixture]
  public class AggressiveDirectionTests
  {
    [Test]
    public void GetDirection_ReturnsValidDirection()
    {
      var aggressiveDirection = new AggressiveDirection();
      bool hasDown = false;
      bool hasUp = false;
      bool hasLeft = false;
      bool hasRight = false;

      for (int i = 0; i < 100; i++)
      {
        Direction direction = aggressiveDirection.GetDirection();
        if (direction == Direction.Down) hasDown = true;
        if (direction == Direction.Up) hasUp = true;
        if (direction == Direction.Left) hasLeft = true;
        if (direction == Direction.Right) hasRight = true;

        if (hasDown && hasUp && hasLeft && hasRight) break;
      }

      Assert.IsTrue(hasDown);
      Assert.IsTrue(hasUp);
      Assert.IsTrue(hasLeft);
      Assert.IsTrue(hasRight);
    }

    [Test]
    public void GetDirection_WithPositions_ReturnsDirectionTowardsTarget()
    {
      var aggressiveDirection = new AggressiveDirection();
      Point hunterPosition = new Point(0, 0);
      Point targetPosition = new Point(1, 1);
      List<Direction> possibleDirections = new List<Direction>();

      for (int i = 0; i < 100; i++)
      {
        Direction direction = aggressiveDirection.GetDirection(hunterPosition, targetPosition);
        possibleDirections.Add(direction);
      }

      Assert.Contains(Direction.Right, possibleDirections);
      Assert.Contains(Direction.Down, possibleDirections);
    }

    [Test]
    public void GetDirection_WithPositions_ReturnsDirectionAwayFromTarget()
    {
      var aggressiveDirection = new AggressiveDirection();
      Point hunterPosition = new Point(1, 1);
      Point targetPosition = new Point(0, 0);
      List<Direction> possibleDirections = new List<Direction>();

      for (int i = 0; i < 100; i++)
      {
        Direction direction = aggressiveDirection.GetDirection(hunterPosition, targetPosition);
        possibleDirections.Add(direction);
      }

      Assert.Contains(Direction.Left, possibleDirections);
      Assert.Contains(Direction.Up, possibleDirections);
    }
  }
}