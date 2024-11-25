using DungeonCrawl.Mechanics;
using NUnit.Framework;
using SadRogue.Primitives;

namespace Tests.Mechanics.Randomisation
{
  [TestFixture]
  public class RandomDirectionTests
  {
    [Test]
    public void GetDirection_ReturnsValidDirection()
    {
      var randomDirection = new RandomDirection();
      bool hasDown = false;
      bool hasUp = false;
      bool hasLeft = false;
      bool hasRight = false;

      for (int i = 0; i < 100; i++)
      {
        Direction direction = randomDirection.GetDirection();
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
    public void GetDirection_WithPositions_ReturnsValidDirection()
    {
      var randomDirection = new RandomDirection();
      Point position = new Point(0, 0);
      Point targetPosition = new Point(1, 1);
      bool hasDown = false;
      bool hasUp = false;
      bool hasLeft = false;
      bool hasRight = false;

      for (int i = 0; i < 100; i++)
      {
        Direction direction = randomDirection.GetDirection(position, targetPosition);
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
  }
}