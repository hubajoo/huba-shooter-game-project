using DungeonCrawl.Mechanics.Randomisation;
using NUnit.Framework;

namespace Tests.Mechanics.Randomisation
{
  [TestFixture]
  public class RandomActionTests
  {
    [Test]
    public void RandomWait_ReturnsValueWithinRange()
    {
      int max = 10;
      for (int i = 0; i < 100; i++)
      {
        int result = RandomAction.RandomWait(max);
        Assert.That(result, Is.InRange(0, max - 1));
      }
    }

    [Test]
    public void Bool_ReturnsTrueOrFalse()
    {
      bool hasTrue = false;
      bool hasFalse = false;

      for (int i = 0; i < 100; i++)
      {
        bool result = RandomAction.Bool();
        if (result) hasTrue = true;
        else hasFalse = true;

        if (hasTrue && hasFalse) break;
      }

      Assert.IsTrue(hasTrue);
      Assert.IsTrue(hasFalse);
    }

    [Test]
    public void WeightedBool_ReturnsTrueOrFalse()
    {
      int max = 10;
      bool hasTrue = false;
      bool hasFalse = false;

      for (int i = 0; i < 100; i++)
      {
        bool result = RandomAction.WeightedBool(max);
        if (result) hasTrue = true;
        else hasFalse = true;

        if (hasTrue && hasFalse) break;
      }

      Assert.IsTrue(hasTrue);
      Assert.IsTrue(hasFalse);
    }

    [Test]
    public void WeightedBool_ReturnsFalse_WhenMaxIsOne()
    {
      int max = 1;
      for (int i = 0; i < 100; i++)
      {
        bool result = RandomAction.WeightedBool(max);
        Assert.IsFalse(result);
      }
    }
  }
}